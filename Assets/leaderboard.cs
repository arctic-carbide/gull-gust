using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class leaderboard : MonoBehaviour {

	private const string PREFIX = "file://";
	private static string path;
	//private static string filename;
	private const string EXTENSION = ".html";
	private string player_name;
	private GameObject player_input;
	
	// Use this for initialization
	void Start () {
		path = Application.streamingAssetsPath;
		player_input = GameObject.Find("Name Entry Box");
		
	}

	public void Load()
	{
		Application.OpenURL(PREFIX + path + "/leaderboard" + EXTENSION);
	}

	public void SubmitName()
	{
		player_name = player_input.GetComponent<InputField>().text;
		new WWW(PREFIX + path + "/leaderboard" + EXTENSION);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
