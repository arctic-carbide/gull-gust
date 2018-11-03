using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneControl : MonoBehaviour {

	private static GameObject main_camera;
	private static Camera mc;

	

	public void Play()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void End()
	{
		main_camera = GameObject.Find("Main Camera");
		SceneManager.LoadScene("MainMenu");

		Destroy(main_camera);
	}

	public void Restart()
	{	
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
