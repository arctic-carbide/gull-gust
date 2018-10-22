using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private const int vertical_force_factor = 50;
	private const int forward_force_factor = 100;

	private Rigidbody2D physical_body;
	private Vector2 upward_force = new Vector2(0, vertical_force_factor);
	private Vector2 downward_force = new Vector2(0, -vertical_force_factor);
	private Vector2 forward_force = new Vector2(forward_force_factor, 0);
	// private int speed = 0; // TODO: SET SPEED EQUAL TO DECCELERATION
	// bool state = false;

	enum State { MOVING_UP, MOVING_DOWN, IDLE };
	// State activity = State.IDLE;


	//private void moveDown() { physical_body.AddForce(downward_force); }
	//private void moveUp() { physical_body.AddForce(upward_force); }
	
	private void moveUp(int force)
	{
		// physical_body.velocity.Set(0, force);
		physical_body.AddForce(upward_force);
		// activity = State.MOVING_UP;
	}
	private void moveDown(int force)
	{
		//physical_body.velocity.Set(0, -force);
		physical_body.AddForce(downward_force);
	}

	private void killVerticalInertia()
	{
		physical_body.AddForce(new Vector2(0, -physical_body.velocity.y));
	}
	
	// Use this for initialization
	void Start () {
		physical_body = this.GetComponent<Rigidbody2D>();
		physical_body.AddForce(forward_force);
		//x.AddForce(new Vector2(0, 10));
	}
	
	private void changePlayerVelocity()
	{

		if (Input.GetKey(KeyCode.W)) { moveUp(vertical_force_factor); }
		if (Input.GetKey(KeyCode.S)) { moveDown(vertical_force_factor); }

		//switch (activity)
		//{
		//	case State.IDLE:
		//		if (Input.GetKey(KeyCode.W)) { moveUp(vertical_force_factor); }
		//		else if (Input.GetKey(KeyCode.S)) { moveDown(vertical_force_factor); }
		//		else { killVerticalInertia(); }
		//		break;
		//	case State.MOVING_DOWN:
		//		if (Input.GetKeyUp(KeyCode.S)) { activity = State.IDLE; }
		//		break;
		//	case State.MOVING_UP:
		//		if (Input.GetKeyUp(KeyCode.W)) { activity = State.IDLE; }
		//		break;
		//	default:
		//		Debug.Log("Reached end of switch case");
		//		break;
		//}
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 position = this.transform.position;
		Quaternion rotation = this.transform.rotation;

		changePlayerVelocity();
	}
}
