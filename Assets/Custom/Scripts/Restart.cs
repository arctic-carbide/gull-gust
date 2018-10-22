using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	GameObject bg;
	Scene s;
	const double minvelocity = -1.0;
	string bg_name = "background";
	

	// Use this for initialization
	void Start () {
		bg = GameObject.Find(bg_name);
		s = SceneManager.GetSceneByBuildIndex(1);
	}
	
	// Update is called once per frame
	void Update () {
		if (bg.GetComponent<Rigidbody2D>().velocity.x >= minvelocity)
		{
			SceneManager.LoadScene("main-menu");
		}
	}
}
