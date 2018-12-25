using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * AUTHOR: MITCHELL MYERS
 * DATE: 12/24/2018
 * 
 */

public class TextField : MonoBehaviour {
	
	public TextMeshProUGUI textBox;
	[TextArea(5, 20)] public string message;
	
	public void ApplyMessageToTextBox()
	{
		textBox.SetText(message);
	}

}
