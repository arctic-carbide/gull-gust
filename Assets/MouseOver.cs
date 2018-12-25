using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseOver : MonoBehaviour {

	public GameObject infoBox;

	[TextArea()]
	public string message;


	private TextMeshProUGUI tm;
	private const string EMPTY = "";

	// Use this for initialization
	void Start() {

		tm = infoBox.GetComponentInChildren<TextMeshProUGUI>();

		if (tm.text != EMPTY)
		{
			tm.SetText(EMPTY);
		}
	}

	private void OnMouseEnter()
	{
		tm.SetText(message);
	}

	private void OnMouseExit()
	{
		tm.SetText(EMPTY);
	}
}
