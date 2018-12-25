using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {
	
	public bool showPanel;
	private Image panel;

	// Use this for initialization
	void Start () {
		if (panel == null)
		{
			panel = GetComponent<Image>();
		}

		panel.enabled = false;
	}

	private void OnValidate()
	{
		panel = GetComponent<Image>();

		if (showPanel)
		{
			panel.enabled = true;
		}
		else
		{
			panel.enabled = false;
		}
	}
}
