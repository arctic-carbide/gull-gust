using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	GameObject bg;
	Scene s;
	const double minvelocity = -1.0;

	// Use this for initialization
	void Start () {
		bg = GameObject.Find("background");
		s = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
		if (bg.GetComponent<Rigidbody2D>().velocity.x >= minvelocity)
		{
			SceneManager.LoadScene(s.buildIndex);
		}
	}
}
