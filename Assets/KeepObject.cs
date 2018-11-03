using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObject : MonoBehaviour {

	private static GameObject keeper;

	// Use this for initialization
	void Start () {
		if (keeper != null)
		{
			Destroy(gameObject);
		}
		else
		{
			keeper = gameObject;
			DontDestroyOnLoad(keeper);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
