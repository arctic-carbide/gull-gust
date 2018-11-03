using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public enum View { MAIN, CREDITS, CONTROLS, INPUT };
	private static Vector2 main_viewpoint = new Vector2(0, 10);
	private static Vector2 credits_viewpoint = new Vector2(0, 0);
	private static Vector2 input_viewpoint = new Vector2(0, -20);
	private static Vector2 controls_viewpoint = new Vector2(0, -10);
	private static GameObject main_camera;
	

	// Use this for initialization
	void Start () {
		main_camera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	public void ChangeView(View index)
	{
		switch (index)
		{
			case View.INPUT:
				main_camera.transform.position = input_viewpoint;
				break;
			case View.CREDITS:
				main_camera.transform.position = credits_viewpoint;
				break;
			case View.CONTROLS:
				main_camera.transform.position = controls_viewpoint;
				break;
			case View.MAIN:
			default:
				main_camera.transform.position = main_viewpoint;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
