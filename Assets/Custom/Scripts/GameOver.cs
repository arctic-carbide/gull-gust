using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private const double MIN_VELOCITY = 1.0;
	private double current_velocity;
	private GameObject bg;
	private GameObject restart_button;
	private GameObject return_button;
	private bool triggered = false;


	// Use this for initialization
	void Start () {
		restart_button = GameObject.Find("Restart Button");
		return_button = GameObject.Find("Exit Button");
		bg = GameObject.Find("background");

		restart_button.SetActive(false);
		return_button.SetActive(false);

		Debug.Log(restart_button.name);
		Debug.Log(return_button.name);
		Debug.Log(bg.name);
	}
	
	private void FixedUpdate()
	{
		current_velocity = Mathf.Abs(bg.GetComponent<Rigidbody2D>().velocity.x);
		if (!triggered && MIN_VELOCITY > current_velocity)
		{
			
			restart_button.SetActive(true);
			return_button.SetActive(true);
			triggered = true;
		}
	}
}
