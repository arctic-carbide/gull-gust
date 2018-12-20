using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GullGust
{
	public class MenuManager : MonoBehaviour
	{

		private GameObject currentMenu;
		private GameObject[] menues;
		private Stack<GameObject> history; // for backtracking
		public GameObject[] enablerList = new GameObject[1];

		// Use this for initialization
		void Start()
		{
			var x = GetComponentsInChildren<Transform>();
			menues = GetGameObjects(x);
			foreach (GameObject menu in menues)
			{
				menu.SetActive(false);
			}

		}

		private GameObject[] GetGameObjects(Component[] x)
		{
			var size = x.Length;
			GameObject[] results = new GameObject[size];

			for (int i = 0; i < size; i++)
			{
				results[i] = x[i].gameObject;
			}

			return results;
		}

		public void Backtrack()
		{
			currentMenu.SetActive(false);

			history.Pop();
			currentMenu = history.Peek();

			currentMenu.SetActive(true);
		}

		public void Navigate(GameObject menu)
		{
			currentMenu.SetActive(false);

			history.Push(menu);
			currentMenu = menu;

			currentMenu.SetActive(true);
		}
	}

}
