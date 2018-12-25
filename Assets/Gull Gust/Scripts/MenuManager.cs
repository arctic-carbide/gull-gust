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
		private Stack<GameObject> history = new Stack<GameObject>();

		private const string DEFAULT_START_BUTTON = "Jump";
		private const string DEFAULT_MENU_TAG = "Menu";
		private const string DEFAULT_RETURN_BUTTON = "Cancel";

		[Header("Buttons")]
		//public GameObject GUIReturnButton; // the button to use for return to previous menues
		public string inputReturnButton = DEFAULT_RETURN_BUTTON;
		public string inputStartButton = DEFAULT_START_BUTTON;
	
		[Header("Startup")]
		public string menuTag = DEFAULT_MENU_TAG;
		public GameObject startMenu;
		public GameObject mainMenu;

		private static GameObject[] menues;
		

		// Use this for initialization
		void Start()
		{
			// when we return to the main menu scene, we want to reuse the same objects
			if (menues == null) // run once
			{
				menues = gameObject.GetGameObjectsWithTagFromComponentsInChildren<Transform>(menuTag, true);
			}
			

			menues.DeactivateAll();

			startMenu.Activate();
			currentMenu = startMenu;
		}

		private void Update()
		{
			DetermineInputNavigation(); // let the script decide how you are allowed to move forward through menues
			DetermineInputBacktracking(); // let the script decide how you are allowed to move backward through menues
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
			if (PressedReturnButton() && !OnStartMenu() && !OnMainMenu())
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

			Debug.Assert(history.Count > 1);
			history.Pop();
			previousMenu = history.Peek();
			
			previousMenu.Activate();
			currentMenu = previousMenu;

			//DetermineReturnButtonDisplay();
		}

		//private void DetermineReturnButtonDisplay()
		//{
		//	if (!OnMainMenu() && !OnStartMenu())
		//	{
		//		GUIReturnButton.Activate();
		//	}
		//	else
		//	{
		//		GUIReturnButton.Deactivate();
		//	}
		//}

		public void NavigateTo(GameObject nextMenu)
		{
			currentMenu.Deactivate();
			history.Push(nextMenu);
			
			nextMenu.Activate();
			currentMenu = nextMenu;

			//DetermineReturnButtonDisplay();
		}
	}

}
