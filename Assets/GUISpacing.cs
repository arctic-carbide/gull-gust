using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: MITCHELL MYERS
 * DATE: 12/21/2018
 * 
 */

namespace GullGust
{
	public class GUISpacing : MonoBehaviour
	{

		public enum Style
		{
			Vertical,
			Horizontal,
			Diagonal
		}

		private RectTransform[] buttons;
		private Vector2 origin;

		public Style arrangement = Style.Vertical;
		public float spacing = 15f;

		private void Start()
		{
			origin = transform.position;
		}

		private void OnValidate()
		{
			buttons = GetComponentsInChildren<RectTransform>();
			switch (arrangement)
			{
				case Style.Vertical:
					ArrangeElementsVertically(ref buttons);
					break;

				case Style.Horizontal:
					ArrangeElementsHorizontally(ref buttons);
					break;

				case Style.Diagonal:
					ArrangeElementsDiagonally(ref buttons);
					break;

				default:
					break;
			}
		}

		private void ArrangeElementsVertically(ref RectTransform[] elements)
		{
			float yPos = origin.y + (spacing * (elements.Length / 2f));
			RectTransform vRect;

			for (int i = 0; i < elements.Length; i++)
			{
				yPos -= spacing * i;
				vRect = elements[i];
			}

		}

		private void ArrangeElementsHorizontally(ref RectTransform[] elements)
		{

		}

		private void ArrangeElementsDiagonally(ref RectTransform[] elements)
		{

		}
	}
}