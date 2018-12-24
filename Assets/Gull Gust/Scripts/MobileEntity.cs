using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: MITCHELL MYERS
 * DATE: 12/10/2018
 * 
 * 
 */

namespace MKM
{

	[RequireComponent(typeof(Rigidbody))] // required to jump
	public class MobileEntity : MonoBehaviour
	{
		public enum ScaleMode { Absolute, Relative };
		public enum SyncMode { Async, Sync };
		public enum MovementState { Idle, Running, Flying, Crawling, Crouching, Sprinting, Falling, Rising }
		public enum ColliderPreference { Box, Sphere, Capsule, Mesh, Character }

		/*	################################
		 *				ATTRIBUTES
		 *	################################
		 */

		[Header("Movement Settings")]
		[Tooltip("Absolute: a 1 meter cube moves 1 meter per second; scaled to 5 meters, it moves 1 meter per second.\n\n" +
						"Relative: a 1 meter cube moves 1 meter per second; scaled to 5 meters, it moves 5 meters per second.")]
		public ScaleMode scalingMode;
		[Tooltip("Async: the amount an object can move is different across all axes of movement.\n\n" +
					"Sync: the amount an object can move is the same across all axes of movement.")]
		public SyncMode movementType;
		[Tooltip("How many units to move forward over time.")]
		public float forwardSpeed = 1f;
		[Tooltip("How many units to move backward over time.")]
		public float backwardSpeed = 1f;
		[Tooltip("How many units to move left or right over time.")]
		public float strafingSpeed = 1f;
		[Tooltip("How hard to push off the ground to jump.")]
		public float jumpingForce = 1f;

		[Header("Collision Settings")]
		[Tooltip("The type of collider to attach to the game object. Must hit 'apply' at the end of the component to see results.")]
		public ColliderPreference preferredCollider;

		[Header("Aim Settings")]
		[Tooltip("How many degrees to turn over time")]
		public float turningSpeed = 1f;
		[Tooltip("How high can I look up or down?")]
		public float xAngleBoundary = 90f;
		[Tooltip("How far can I lean left or right?")]
		public float zAngleBoundary = 45f;
		[Tooltip("How far can I detect the ground underneath my game object?")]
		public float downcastMagnitude = 0.001f;

		[Header("Targetting Settings")]
		[Tooltip("The target object to perform rotations on.")]
		public GameObject cameraPivotPoint;

		protected MovementState currentMovementState; // how the entity is moving right now
		protected Collider attachedCollider;
		protected Rigidbody attachedRigidbody;
		protected Ray downcastRay; // the ray used for detecting if the entity is touching the floor enough to jump again

		protected float minimumTransformScale; // the smallest scale of the 3 major scales
		protected float adjustedForwardSpeed; // the actual forward speed after applying modifiers
		protected float adjustedBackwardSpeed; // the actual backward speed after applying modifiers
		protected float adjustedStrafingSpeed; // the actual strafing speed after applying modifiers
		protected float adjustedJumpingForce; // the actual jumping force after applying modifiers
		protected float adjustedDowncastMagnitude; // the actual casting magnitude after applying modifiers

		protected float xDefinedQ1AngleBoundary; // the highest angle about the x axis defined to restrict looking between a bound
		protected float xDefinedQ4AngleBoundary; // the lowest angle about the x axis defined to restrict looking between a bound
		protected float zDefinedQ1AngleBoundary; // the highest angle about the z axis defined to restrict leaning between a bound
		protected float zDefinedQ4AngleBoundary; // the lowest angle about the z axis defined to restrict leaning between a bound
		protected readonly float xUpperQ1AngleBoundary = 360f; // the angle that is equivalent to looking dead ahead
		protected readonly float xLowerQ1AngleBoundary = 360f - 90f; // the angle that is equivalent to looking all the way up
		protected readonly float xUpperQ4AngleBoundary = 0f; // the angle that is also equivalent to looking dead ahead
		protected readonly float xLowerQ4AngleBoundary = 90f; // the angle that is equivalent to looking all the way down
		protected readonly float zUpperQ1AngleBoundary = 360f; // the angle that is equivalent to not leaning
		protected readonly float zLowerQ1AngleBoundary = 360f - 90f; // the angle that is equivalent to leaning all the way right
		protected readonly float zUpperQ4AngleBoundary = 0f; // the angle that is again equivalent to not leaning
		protected readonly float zLowerQ4AngleBoundary = 90f; // the angle that is equivalent to leaning all the way left

		protected GameObject angler; // determines how high i can look up or down based on how far it has traveled in the game space
		public GameObject leaningPivotPoint;
		static int idNumber = 0;

		/*	################################
		 * 				PROPERTIES
		 * 	################################
		 */

		public Collider AttachedCollider
		{
			get
			{
				return attachedCollider;
			}

			protected set
			{
				attachedCollider = value;
			}
		}

		public Rigidbody AttachedRigidbody
		{
			get
			{
				return attachedRigidbody;
			}

			protected set
			{
				attachedRigidbody = value;
			}
		}

		/*	################################
		 *				EVENTS
		 *	################################ 
		 */

		public virtual void Start()
		{
			Initialize(out attachedRigidbody);
			Initialize(out attachedCollider);
			Initialize(out currentMovementState);

			Initialize(scalingMode);
			Initialize(downcastRay, transform.position, Vector3.down);
			InitializeAngleBoundaries();

			++idNumber;
			angler = new GameObject(name + ": Angler" + idNumber);
			angler.transform.position = Vector3.zero; // put this at the origin
		}

		public virtual void FixedUpdate()
		{
			ApplyForces(scalingMode);
		}

		public virtual void Update()
		{
			Debug.DrawRay(transform.position, Vector3.down * adjustedDowncastMagnitude);
		}

		public virtual void OnValidate()
		{
			AddDefaultCollider();
			ApplyMovementBehavior();
		}


		/*	################################
		 *			INITIALIZERS
		 *	################################
		 */

		public virtual void Initialize<Type>(out Type x)
		{
			x = GetComponent<Type>();
		}

		public virtual void Initialize(Ray ray, Vector3 origin, Vector3 dir)
		{
			ray = new Ray(origin, dir);
		}

		public virtual void InitializeAngleBoundaries()
		{
			xDefinedQ4AngleBoundary = xAngleBoundary;
			xDefinedQ1AngleBoundary = xUpperQ1AngleBoundary - xAngleBoundary;
			zDefinedQ4AngleBoundary = zAngleBoundary;
			zDefinedQ1AngleBoundary = zUpperQ1AngleBoundary - zAngleBoundary;
		}

		public virtual void Initialize(ScaleMode mode)
		{
			adjustedDowncastMagnitude = downcastMagnitude + attachedCollider.bounds.extents.y;
			switch (mode)
			{
				case ScaleMode.Relative: // collected values should be modified to provide a relative effect

					var xScale = transform.localScale.x;
					var yScale = transform.localScale.y;
					var zScale = transform.localScale.z;
					minimumTransformScale = Mathf.Min(xScale, yScale, zScale);

					adjustedForwardSpeed = forwardSpeed * minimumTransformScale;
					adjustedBackwardSpeed = backwardSpeed * minimumTransformScale;
					adjustedStrafingSpeed = strafingSpeed * minimumTransformScale;
					adjustedJumpingForce = jumpingForce * minimumTransformScale;

					break;

				case ScaleMode.Absolute: // collected values are the adjusted values

					adjustedForwardSpeed = forwardSpeed;
					adjustedBackwardSpeed = backwardSpeed;
					adjustedStrafingSpeed = strafingSpeed;
					adjustedJumpingForce = jumpingForce;

					break;

				default: // do nothing

					break;
			}
		}

		public virtual void Initialize(out MovementState state)
		{
			state = MovementState.Idle;
		}

		/*	################################
		 *				METHODS
		 *	################################
		 */

		public virtual void ReplaceCollider()
		{
			foreach (Collider x in FindObjectsOfType<Collider>()) // kills all colliders that may be attached to the game object
			{
				DestroyImmediate(x);	
			}

			switch (preferredCollider) // replaces the dead collider with a new one
			{
				case ColliderPreference.Box: // replace current collider with box
					gameObject.AddComponent<BoxCollider>();
					break;

				case ColliderPreference.Sphere: // replace current collider with sphere
					gameObject.AddComponent<SphereCollider>();
					break;

				case ColliderPreference.Capsule: // replace current collider with capsule
					gameObject.AddComponent<CapsuleCollider>();
					break;

				case ColliderPreference.Mesh: // replace current collider with mesh
					gameObject.AddComponent<MeshCollider>();
					break;

				case ColliderPreference.Character:
					gameObject.AddComponent<CharacterController>();
					break;

				default: // log to the console that the option is not available and that no action was taken
					Debug.Log("MobileEntity.ColliderReplace: there is no action available for the desired collider specified. Please select a different collider type.");
					break;
			}

			attachedCollider = gameObject.GetComponent<Collider>(); // replace the reference of old collider with new collider
		}

		public virtual void ApplyForces(ScaleMode mode)
		{
			switch (mode)
			{
				case ScaleMode.Relative: // apply artifical gravity to the attached object
					attachedRigidbody.AddForce(Physics.gravity * minimumTransformScale);
					break;

				case ScaleMode.Absolute: // do nothing
					break;

				default: // do nothing
					break;
			}
		}

		public virtual bool CanJump()
		{
			var grounded = Physics.Raycast(downcastRay, adjustedDowncastMagnitude);
			if (!grounded) {
				currentMovementState = MovementState.Flying;
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Move the attached object 'x' units left (framerate indepenedent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveLeft(float speed = 1f)
		{
			transform.Translate(Vector3.left * speed * Time.deltaTime);
			currentMovementState = MovementState.Running;
		}

		/// <summary>
		/// Move the attached object 'x' units right (framerate indepenedent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveRight(float speed = 1f)
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			currentMovementState = MovementState.Running;
		}

		/// <summary>
		/// Move the attached object 'x' units forward (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveForward(float speed = 1f)
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			currentMovementState = MovementState.Running;
		}

		/// <summary>
		/// Move the attached object 'x' units backward (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveBackward(float speed = 1f)
		{
			transform.Translate(Vector3.back * speed * Time.deltaTime);
			currentMovementState = MovementState.Running;
		}

		/// <summary>
		/// Move the attached object 'x' units up (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveUp(float speed = 1f)
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
			currentMovementState = MovementState.Rising;
		}

		/// <summary>
		/// Move the attached object 'x' units down (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void MoveDown(float speed = 1f)
		{
			transform.Translate(Vector3.down * speed * Time.deltaTime);
			currentMovementState = MovementState.Falling;
		}

		/// <summary>
		/// Rotate the attached object 'x' degrees clockwise (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void TurnRight(float degrees = 1f)
		{
			transform.Rotate(Vector3.up * degrees * Time.deltaTime);
		}

		/// <summary>
		/// Rotate the attached object 'x' degrees counter-clockwise (framerate independent).
		/// </summary>
		/// <param name="speed"></param>
		public virtual void TurnLeft(float degrees = 1f)
		{
			transform.Rotate(Vector3.down * degrees * Time.deltaTime);
		}

		/// <summary>
		/// Make the attached object jump 'x' units into the air.
		/// </summary>
		/// <param name="speed"></param>
		public virtual void Jump(float height = 1f, ForceMode mode = ForceMode.Impulse)
		{
			if (currentMovementState != MovementState.Flying)
			{
				attachedRigidbody.AddForce(Vector3.up * height, mode);
			}
		}

		public virtual void LookUp(float degrees = 1f)
		{
			//var xAngle = pivotPoint.transform.rotation.eulerAngles.x;

			// xAngle = Mathf.Clamp(xAngle, xUpperQ1AngleBoundary, xLowerQ1AngleBoundary);
			// Debug.Log(xAngle);
			//var q1 = (xAngle >= 360f && xAngle < 270f - 0.01f);
			//var q4 = (xAngle >= 0f && xAngle < 90f - 0.01f);

			// Debug.Log(angler.transform.position.y);
			if (angler.transform.position.y < 90f)
			{
				var mag = Vector3.left * degrees * Time.deltaTime;
				angler.transform.Translate(Vector3.up * degrees * Time.deltaTime);
				cameraPivotPoint.transform.Rotate(mag);
				
			}
		}

		public virtual void LookDown(float degrees = 1f)
		{
			//var xAngle = pivotPoint.transform.rotation.eulerAngles.x;

			// xAngle = Mathf.Clamp(xAngle, xUpperQ4AngleBoundary, xLowerQ4AngleBoundary);
			// Debug.Log(xAngle);
			//var q1 = (xAngle >= 360f && xAngle < 270f - 0.01f);
			//var q4 = (xAngle >= 0f && xAngle < 90f - 0.01f);

			// Debug.Log(angler.transform.position.y);

			Debug.Log(angler.transform.position.z);
			if (angler.transform.position.y > -90f)
			{
				angler.transform.Translate(Vector3.down * degrees * Time.deltaTime);
				var mag = Vector3.right * degrees * Time.deltaTime;
				cameraPivotPoint.transform.Rotate(mag);
			}
		}

		public virtual void LeanLeft(float degrees = 1f)
		{
			var zAngle = cameraPivotPoint.transform.rotation.eulerAngles.z;

			// zAngle = Mathf.Clamp(zAngle, zUpperQ1AngleBoundary, zLowerQ1AngleBoundary);
			Debug.Log(angler.transform.position.z);
			if (angler.transform.position.z > -15f)
			{
				var mag = Vector3.back * degrees * Time.deltaTime;
				var point = leaningPivotPoint.transform.position;
				var axis = leaningPivotPoint.transform.forward * -1;

				angler.transform.Translate(mag);
				transform.RotateAround(point, axis, degrees * Time.deltaTime);
			}
		}

		public virtual void LeanRight(float degrees = 1f)
		{
			//var zAngle = pivotPoint.transform.rotation.eulerAngles.z;

			// zAngle = Mathf.Clamp(zAngle, zUpperQ4AngleBoundary, zLowerQ4AngleBoundary);
			if (angler.transform.position.z < 15f)
			{
				var mag = Vector3.forward * degrees * Time.deltaTime;
				var point = leaningPivotPoint.transform.position;
				var axis = leaningPivotPoint.transform.forward;

				angler.transform.Translate(mag);
				transform.RotateAround(point, axis, degrees * Time.deltaTime);
			}
		}

		public virtual void Strafe(float magnitude = 1f)
		{
			if (magnitude > 0f)
			{
				MoveRight(magnitude);
			}
			else
			{
				magnitude = -magnitude; // invert
				MoveLeft(magnitude);
			}
		}

		public virtual void Run(float magnitude = 1f)
		{
			if (magnitude > 0f)
			{
				MoveForward(magnitude);
			}
			else
			{
				magnitude = -magnitude;
				MoveBackward(magnitude);
			}
		}

		public virtual void Turn(float degrees = 1f)
		{
			if (degrees > 0f)
			{
				TurnRight(degrees);
			}
			else
			{
				degrees = -degrees;
				TurnLeft(degrees);
			}
		}

		public virtual void Look(float degrees = 1f)
		{
			if (degrees > 0f)
			{
				LookUp(degrees);
			}
			else
			{
				degrees = -degrees;
				LookDown(degrees);
			}
		}

		public virtual void Lean(float degrees = 1f)
		{
			if (degrees > 0f)
			{
				LeanRight(degrees);
			}
			else if (degrees < 0f)
			{
				degrees = -degrees;
				LeanLeft(degrees);
			}
			else
			{
				if (angler.transform.position.z > 0.5)
				{
					LeanLeft(turningSpeed);
				}
				else if (angler.transform.position.z < 0.5)
				{
					LeanRight(turningSpeed);
				}
				else
				{
					// TODO: force rotation to zero

				}
			
			}
		}

		public virtual void AddDefaultCollider()
		{
			var collider = gameObject.GetComponent<Collider>();
			if (collider == null)
			{
				gameObject.AddComponent<BoxCollider>();
			}
		}

		public virtual void ApplyMovementBehavior()
		{
			if (movementType == SyncMode.Sync)
			{
				backwardSpeed = forwardSpeed;
				strafingSpeed = forwardSpeed;
			}
		}
	}
}
