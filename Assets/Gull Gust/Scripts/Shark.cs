using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: MITCHELL MYERS
 * DATE: 12/21/2018
 * 
 */

namespace GullGust
{
	public class Shark : MonoBehaviour
	{

		private const float UPPER_RADIAN_BOUND = 2f * Mathf.PI;
		private const float LOWER_RADIAN_BOUND = 0f;
		private float radians = LOWER_RADIAN_BOUND;
		public float amplitude; // controls how high the shark moves up and down
		public float frequency; // controls how fast the shark moves up and down

		private void FixedUpdate()
		{
			IncrementRadians(0.001f);			
		}

		private void Update()
		{
			Oscillate();
		}

		private void Oscillate()
		{
			var xPos = transform.position.x;
			var yPos = Mathf.Sin(radians * frequency) * amplitude;
			Vector2 nPos = new Vector2(xPos, yPos);

			var xRot = transform.rotation.eulerAngles.x;
			var yRot = transform.rotation.eulerAngles.y;
			var zRot = Mathf.Cos(radians * frequency) * amplitude;
			Quaternion nRot = Quaternion.Euler(xRot, yRot, zRot);

			transform.SetPositionAndRotation(nPos, nRot);
		}

		private void IncrementRadians(float increment)
		{
			if (radians < UPPER_RADIAN_BOUND)
			{
				radians += increment;
			}
			else
			{
				radians = LOWER_RADIAN_BOUND;
			}
		}
	}

}
