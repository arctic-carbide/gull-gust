using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Mitchell Myers
 * DATE: 12/21/2018
 * 
 */

namespace GullGust
{
	public class MenuManager : MonoBehaviour
	{
		private GameObject currentMenu;
		private Stack<GameObject> history; // for backtracking		
		private const string DEFAULT_START_BUTTON = "Jump";
		private const string DEFAULT_TARGET_TAG = "Menu";
		private const string DEFAULT_RETURN_BUTTON = "Cancel";

		[Header("Buttons")]
		public GameObject GUIReturnButton; // the button to use for return to previous menues
		public string inputReturnButton = DEFAULT_RETURN_BUTTON;
		public string inputStartButton = DEFAULT_START_BUTTON;
	
		[Header("Startup")]
		public string targetMenuTag = DEFAULT_TARGET_TAG;
		public GameObject startMenu;
		public GameObject mainMenu;

		[Header("Debug")]
		public bool showPanels = true;
		

		// Use this for initialization
		void Start()
		{
			GameObject[] menues = GameObject.FindGameObjectsWithTag(targetMenuTag);
			Image[] panels = GetComponentsInChildren<Image>();

			DisableAll(menues);
			DisableAll(panels);
			Enable(startMenu);
			currentMenu = startMenu;
		}

		private void Update()
		{
			DetermineInputNavigation(); // let the script decide how you are allowed to move forward through menues
			DetermineInputBacktracking(); // let the script decide how you are allowed to move backward through menues
		}

		private void OnValidate()
		{
			Image[] panels = GetComponentsInChildren<Image>();

			if (showPanels)
			{
				EnableAll(panels);
			}
			else
			{
				DisableAll(panels);
			}
		}

		private void DetermineInputNavigation()
		{
			if (OnStartMenu() && PressedAnyButton())
			{
				NavigateTo(mainMenu);
			}
		}

		private void DetermineInputBacktracking()
		{
			if (!OnStartMenu() && !OnMainMenu() && PressedReturnButton())
			{
				Backtrack();
			}
		}		

		private bool OnStartMenu() { return (currentMenu == startMenu); }
		private bool OnMainMenu() { return (currentMenu == mainMenu); }
		private bool PressedStartButton() { return Input.GetButtonDown(inputStartButton); }
		private bool PressedReturnButton() { return Input.GetButtonDown(inputReturnButton); }
		private bool PressedAnyButton() { return Input.anyKeyDown; }

		public void Backtrack()
		{
			GameObject previousMenu;
			Disable(currentMenu);

			Debug.Assert(history.Count > 1);
			history.Pop();
			previousMenu = history.Peek();

			Enable(previousMenu);
			currentMenu = previousMenu;

			DetermineReturnButtonDisplay();
		}

		private void DetermineReturnButtonDisplay()
		{
			if (!OnMainMenu() && !OnStartMenu())
			{
				Enable(GUIReturnButton);
			}
			else
			{
				Disable(GUIReturnButton);
			}
		}

		public void NavigateTo(GameObject nextMenu)
		{
			Disable(currentMenu);
			history.Push(nextMenu);

			Enable(nextMenu);
			currentMenu = nextMenu;

			DetermineReturnButtonDisplay();
		}

		private void Enable(GameObject x)
		{
			x.SetActive(true);
		}

		private void Disable(GameObject x)
		{
			x.SetActive(false);
		}

		private void DisableAll(GameObject[] x)
		{
			foreach (GameObject g in x)
			{
				Disable(g);
			}
		}

		private void DisableAll(Behaviour[] x)
		{
			foreach (Behaviour b in x)
			{
				Disable(b);
			}
		}

		private void Disable(Behaviour b)
		{
			b.enabled = false;
		}

		private void DisableAll(Image[] x)
		{
			foreach (Image i in x)
			{
				if (i.gameObject.CompareTag("Panel"))
				{
					Disable(i);
				}
			}
		}

		private void Enable(Behaviour b)
		{
			b.enabled = true;
		}

		private void EnableAll(Image[] x)
		{
			foreach (Image i in x)
			{
				if (i.gameObject.CompareTag("Panel"))
				{
					Enable(i);
				}
			}
		}
	}

}
