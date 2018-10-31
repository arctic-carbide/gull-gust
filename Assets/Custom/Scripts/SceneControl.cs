using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneControl : MonoBehaviour {




	public void play()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void end()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
	}
}
