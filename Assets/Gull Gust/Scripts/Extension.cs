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
				if (gameObject.TaggedWith(tag))
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
				if (gameObject.NotTaggedWith(tag))
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
				if (gameObject.TaggedWith(tag))
				{
					gameObject.Deactivate();
				}
			}
		}

		public static void DeactivateAllWithoutTag(this GameObject[] gameObjects, string tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.NotTaggedWith(tag))
				{
					gameObject.Deactivate();
				}
			}
		}

		public static void SetAllWithTagActive(this GameObject[] gameObjects, string tag, bool flag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.TaggedWith(tag))
				{
					gameObject.SetActive(flag);
				}
			}
		}

		public static void SetAllWithoutTagActive(this GameObject[] gameObjects, string tag, bool flag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.NotTaggedWith(tag))
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
				if (behaviour.TaggedWith(tag))
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
				if (behaviour.NotTaggedWith(tag))
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
				if (behaviour.TaggedWith(tag))
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
				if (behaviour.NotTaggedWith(tag))
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
				if (behaviour.TaggedWith(tag))
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
				if (behaviour.NotTaggedWith(tag))
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

		public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag, bool includeInactive = false) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>(includeInactive);
			List<T> children = new List<T>(ts);

			foreach (T t in ts)
			{
				if (t.NotTaggedWith(tag)) // if the object does not have the specified
				{
					children.Remove(t); // remove it, because we want only objects with that tag returned
				}
			}
			
			// we want to return the array of objects so that functionality between built-in unity methods persists
			// we also want to return null if the list contains no objects, as most built-in unity methods do
			return children.Count > 0 ? children.ToArray() : null;
		}

		public static T[] GetComponentsInChildrenWithoutTag<T>(this GameObject gameObject, string tag, bool includeInactive = false) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>(includeInactive);
			List<T> children = new List<T>(ts);

			foreach (T t in ts)
			{
				if (t.TaggedWith(tag)) // check if an object has the tag
				{
					children.Remove(t); // remove the tagged object, because we only want the objects that do not have that tag
				}
			}

			return children.Count > 0 ? children.ToArray() : null;
		}
		
		//public static T[] GetComponentsInChildrenByTag<T>
		//	(this GameObject gameObject, string tag, bool includeInactive = false, bool includeTag = true) where T : Component
		//{
		//	T[] children = new T[0];

		//	switch (includeTag)
		//	{
		//		case true:

		//			children = gameObject.GetComponentsInChildrenWithTag<T>(tag, includeInactive);
		//			break;

		//		case false:

		//			children = gameObject.GetComponentsInChildrenWithoutTag<T>(tag, includeInactive);
		//			break;
		//	}

		//	return children;
		//}

		public static T GetComponentInChildrenWithTag<T>(this GameObject gameObject, string tag, bool includeInactive = false) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>(includeInactive);

			foreach (T t in ts)
			{
				if (t.TaggedWith(tag))
				{
					return t; // return the first instance that matches
				}
			}

			return null; // no match found
		}

		public static T GetComponentInChildrenWithoutTag<T>(this GameObject gameObject, string tag, bool includeInactive = false) where T : Component
		{
			T[] ts = gameObject.GetComponentsInChildren<T>(includeInactive);

			foreach (T t in ts)
			{
				if (t.NotTaggedWith(tag))
				{
					return t;
				}
			}

			return null;
		}

		public static bool TaggedWith(this GameObject obj, string tag)
		{
			return obj.CompareTag(tag);
		}

		public static bool NotTaggedWith(this GameObject obj, string tag)
		{
			return !obj.CompareTag(tag);
		}

		public static bool TaggedWith(this Component component, string tag)
		{
			return component.CompareTag(tag);
		}

		public static bool NotTaggedWith(this Component component, string tag)
		{
			return !component.CompareTag(tag);
		}

		public static void DisableComponent<T>(this GameObject gameObject) where T : Behaviour
		{
			T component = gameObject.GetComponent<T>();
			if (component.Exists())
			{
				component.Disable();
			}
			else
			{
				throw new System.Exception("Cannot disable component: component does not exist for " + gameObject.ToString());
			}
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
				throw new System.Exception("Cannot modify component: component does not exist for " + gameObject.ToString());
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
				if (gameObject.TaggedWith(tag))
				{
					gameObject.SetComponentEnabled<T>(flag);
				}
			}
		}

		public static void SetAllComponentsWithoutTagEnabled<T>(this GameObject[] gameObjects, string tag, bool flag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.NotTaggedWith(tag))
				{
					gameObject.SetComponentEnabled<T>(flag);
				}
			}

		}

		public static void DisableAllComponentsWithTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.TaggedWith(tag))
				{
					gameObject.DisableComponent<T>();
				}
			}
		}

		public static void EnableAllComponentsWithTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.TaggedWith(tag))
				{
					gameObject.EnableComponent<T>();
				}
			}
		}

		public enum SetMode { Inclusive, Exclusive }
		public static void DisableAllComponentsByTag<T>(this GameObject[] gameObjects, string tag, SetMode mode = SetMode.Inclusive) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				switch (mode)
				{
					case SetMode.Inclusive:

						gameObjects.DisableAllComponentsWithTag<T>(tag);
						break;

					case SetMode.Exclusive:

						gameObjects.DisableAllComponentsWithoutTag<T>(tag);
						break;

					default:
						string message = "Trying to apply new SetMode to disable components by tag; no changes were made.";

						Debug.Log(message);
						break;
				}
			}
		}

		public static void DisableAllComponentsWithoutTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.NotTaggedWith(tag))
				{
					gameObject.DisableComponent<T>();
				}
			}
		}

		public static void EnableAllComponentsWithoutTag<T>(this GameObject[] gameObjects, string tag) where T : Behaviour
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.NotTaggedWith(tag))
				{
					gameObject.EnableComponent<T>();
				}
			}
		}

		public static GameObject[] GetGameObjects(this Component[] components)
		{
			List<GameObject> gameObjects = new List<GameObject>();

			foreach (Component component in components)
			{
				gameObjects.Add(component.gameObject);
			}

			return gameObjects.ToArray();
		}

		public static GameObject[] GetGameObjectsFromComponentsInChildren<T>(this GameObject gameObject, bool includeInactive = false) where T : Component
		{
			T[] components = gameObject.GetComponentsInChildren<T>(includeInactive);
			GameObject[] gameObjects = components.GetGameObjects();

			return gameObjects;
		}

		public static GameObject[] GetGameObjectsWithTagFromComponentsInChildren<T>(this GameObject gameObject, string tag, bool includeInactive = false)
			where T : Component
		{
			T[] components = gameObject.GetComponentsInChildrenWithTag<T>(tag, includeInactive);
			GameObject[] gameObjects = components.GetGameObjects();

			return gameObjects;
		}
	}

}
