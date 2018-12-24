using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: MITCHELL MYERS
 * DATE: 12/24/2018
 * 
 * 
 */

namespace GullGust
{
	public static class Extension
	{
		public static void SetAllActive(this GameObject[] gameObjects, bool flag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.SetActive(flag);
			}
		}

		public static void Activate(this GameObject gameObject)
		{
			gameObject.SetActive(true);
		}

		public static void Deactivate( this GameObject gameObject)
		{
			gameObject.SetActive(false);
		}

		public static void ActivateAll(this GameObject[] gameObjects)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				//gameObject.SetActive(true);
				gameObject.Activate();
			}
		}

		public static void DeactivateAll(this GameObject[] gameObjects)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				//gameObject.SetActive(false);
				gameObject.Deactivate();
			}
		}

		public static void ActivateAllWithTag(this GameObject[] gameObjects, string tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					//gameObject.SetActive(true);
					gameObject.Activate();
				}
			}
		}

		public static void ActivateAllWithoutTag(this GameObject[] gameObjects, string tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					//gameObject.SetActive(true);
					gameObject.Activate();
				}
			}
		}

		public static void DeactivateAllWithTag(this GameObject[] gameObjects, string tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.Activate();
				}
			}
		}

		public static void DeactivateAllWithoutTag(this GameObject[] gameObjects, string tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.Deactivate();
				}
			}
		}

		public static void SetAllWithTagActive(this GameObject[] gameObjects, string tag, bool flag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					gameObject.SetActive(flag);
				}
			}
		}

		public static void SetAllWithoutTagActive(this GameObject[] gameObjects, string tag, bool flag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.SetActive(flag);
				}
			}
		}

		public static void SetEnabled(this Behaviour behaviour, bool flag)
		{
			behaviour.enabled = flag;
		}

		public static void SetAllEnabled(this Behaviour[] behaviours, bool flag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				//behaviour.enabled = flag;
				behaviour.SetEnabled(flag);
			}
		}

		public static void Disable(this Behaviour behaviour)
		{
			//behaviour.enabled = false;
			behaviour.SetEnabled(false);
		}

		public static void Enable(this Behaviour behaviour)
		{
			//behaviour.enabled = true;
			behaviour.SetEnabled(true);
		}

		public static void DisableAll(this Behaviour[] behaviours)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				//behaviour.enabled = false;
				behaviour.Disable();
			}
		}

		public static void EnableAll(this Behaviour[] behaviours)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				//behaviour.enabled = true;
				behaviour.Enable();
			}
		}

		public static void EnableAllWithTag(this Behaviour[] behaviours, string tag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.HasTag(tag))
				{
					//behaviour.enabled = true;
					behaviour.Enable();
				}
			}
		}

		public static void EnableAllWithoutTag(this Behaviour[] behaviours, string tag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.LacksTag(tag))
				{
					//behaviour.enabled = true;
					behaviour.Enable();
				}
			}
		}

		public static void DisableAllWithTag(this Behaviour[] behaviours, string tag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.HasTag(tag))
				{
					//behaviour.enabled = false;
					behaviour.Disable();
				}
			}
		}

		public static void DisableAllWithoutTag(this Behaviour[] behaviours, string tag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.LacksTag(tag))
				{
					//behaviour.enabled = false;
					behaviour.Disable();
				}
			}
		}

		public static void SetEnabledAllWithTag(this Behaviour[] behaviours, string tag, bool flag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.HasTag(tag))
				{
					//behaviour.enabled = flag;
					behaviour.SetEnabled(flag);
				}
			}
		}

		public static void SetEnabledAllWithoutTag(this Behaviour[] behaviours, string tag, bool flag)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				if (behaviour.LacksTag(tag))
				{
					//behaviour.enabled = flag;
					behaviour.SetEnabled(flag);
				}
			}

		}

		public static bool Exists(this object x)
		{
			return (x != null);
		}

		public static bool ExistsNot(this object x)
		{
			return (x == null);
		}

		public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>();
			List<T> children = new List<T>(ts);

			foreach (T t in ts)
			{
				if (t.LacksTag(tag))
				{
					children.Remove(t);
				}
			}

			if (children.Count > 0)
			{
				return children.ToArray();
			}
			else
			{
				return null; // none of the children had the specified tag, so the list is empty
			}
		}

		public static T[] GetComponentsInChildrenWithoutTag<T>(this GameObject gameObject, string tag) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>();
			List<T> children = new List<T>(ts);

			foreach (T t in ts)
			{
				if (t.HasTag(tag))
				{
					children.Remove(t);
				}
			}

			if (children.Count > 0)
			{
				return children.ToArray();
			}
			else
			{
				return null; // none of the children had the specified tag, so the list is empty
			}
		}

		public static T GetComponentInChildrenWithTag<T>(this GameObject gameObject, string tag) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>();

			foreach (T t in ts)
			{
				if (t.HasTag(tag))
				{
					return t; // return the first instance that matches
				}
			}

			return null; // no match found
		}

		public static bool HasTag(this GameObject obj, string tag)
		{
			return obj.CompareTag(tag);
		}

		public static bool HasTag(this Component component, string tag)
		{
			return component.CompareTag(tag);
		}

		public static bool LacksTag(this GameObject obj, string tag)
		{
			return !obj.CompareTag(tag);
		}

		public static bool LacksTag(this Component component, string tag)
		{
			return !component.CompareTag(tag);
		}

		public static void DisableComponent<T>(this GameObject gameObject) where T : Behaviour
		{
			T component = gameObject.GetComponent<T>();
			component.Disable();
		}

		public static void EnableComponent<T>(this GameObject gameObject) where T : Behaviour
		{
			T component = gameObject.GetComponent<T>();
			if (component.Exists())
			{
				component.Enable();
			}
			else
			{
				throw new System.Exception("Cannot enable component: component does not exist for " + gameObject.ToString());
			}
		}

		public static void SetComponentEnabled<T>(this GameObject gameObject, bool flag) where T : Behaviour
		{
			T component = gameObject.GetComponent<T>();
			if (component.Exists())
			{
				component.SetEnabled(flag);
			}
			else
			{
				throw new System.Exception("Cannot disable component: component does not exist for " + gameObject.ToString());
			}
		}

		public static void DisableAllComponents<T>(this GameObject[] gameObjects) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.DisableComponent<T>();
			}
		}

		public static void EnableAllComponents<T>(this GameObject[] gameObjects) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.EnableComponent<T>();
			}
		}

		public static void SetAllComponentsEnabled<T>(this GameObject[] gameObjects, bool flag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.SetComponentEnabled<T>(flag);
			}
		}

		public static void SetAllComponentsWithTagEnabled<T>(this GameObject[] gameObjects, string tag, bool flag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					gameObject.SetComponentEnabled<T>(flag);
				}
			}
		}

		public static void SetAllComponentsWithoutTagEnabled<T>(this GameObject[] gameObjects, string tag, bool flag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.SetComponentEnabled<T>(flag);
				}
			}

		}

		public static void DisableAllComponentsWithTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					gameObject.DisableComponent<T>();
				}
			}
		}

		public static void EnableAllComponentsWithTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					gameObject.EnableComponent<T>();
				}
			}
		}

		public static void DisableAllComponentsWithoutTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.DisableComponent<T>();
				}
			}
		}

		public static void EnableAllComponentsWithoutTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.LacksTag(tag))
				{
					gameObject.EnableComponent<T>();
				}
			}
		}
	}

}
