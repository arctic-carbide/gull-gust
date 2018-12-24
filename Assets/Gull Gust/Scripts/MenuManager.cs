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
		private string panelTag = "Panel";
		

		// Use this for initialization
		void Start()
		{
			GameObject[] menues = GameObject.FindGameObjectsWithTag(targetMenuTag);
			GameObject[] panels = GameObject.FindGameObjectsWithTag(panelTag);
			//Image[] images = GetComponentsInChildren<Image>();
			//DisableAll(menues);
			//DisableAll(panels);
			//Enable(startMenu);

			menues.DeactivateAll();
			panels.SetAllComponentsWithTagEnabled<Image>(panelTag, false);
			//images.DisableAllWithTag("Panel");

			startMenu.Activate();
			currentMenu = startMenu;
		}

		private void Update()
		{
			DetermineInputNavigation(); // let the script decide how you are allowed to move forward through menues
			DetermineInputBacktracking(); // let the script decide how you are allowed to move backward through menues
		}

		private void OnValidate()
		{
			//Image[] images = GetComponentsInChildren<Image>();
			GameObject[] panels = GameObject.FindGameObjectsWithTag(panelTag);

			if (showPanels)
			{
				//EnableAll(panels);
				//images.SetEnabledAllWithTag("Panel", true);
				//images.EnableAllWithTag("Panel");
				panels.SetAllComponentsWithTagEnabled<Image>(panelTag, true);
			}
			else
			{
				//DisableAll(panels);
				//images.SetEnabledAllWithTag("Panel", false);
				//images.DisableAllWithTag("Panel");
				panels.SetAllComponentsWithTagEnabled<Image>(panelTag, false);
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
			currentMenu.Deactivate();
			//Disable(currentMenu);

			Debug.Assert(history.Count > 1);
			history.Pop();
			previousMenu = history.Peek();

			//Enable(previousMenu);
			//previousMenu.Activate();
			previousMenu.Activate();
			currentMenu = previousMenu;

			DetermineReturnButtonDisplay();
		}

		private void DetermineReturnButtonDisplay()
		{
			if (!OnMainMenu() && !OnStartMenu())
			{
				//Enable(GUIReturnButton);
				GUIReturnButton.Activate();
			}
			else
			{
				//Disable(GUIReturnButton);
				GUIReturnButton.Deactivate();
			}
		}

		public void NavigateTo(GameObject nextMenu)
		{
			//Disable(currentMenu);
			currentMenu.Deactivate();
			history.Push(nextMenu);

			//Enable(nextMenu);
			nextMenu.Activate();
			currentMenu = nextMenu;

			DetermineReturnButtonDisplay();
		}
	}

}
