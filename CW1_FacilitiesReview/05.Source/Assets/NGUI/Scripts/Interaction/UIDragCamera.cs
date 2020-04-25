﻿//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Allows dragging of the WcCamera object and restricts WcCamera's movement to be within bounds of the area created by the rootForBounds colliders.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	/// <summary>
	/// Target object that will be dragged.
	/// </summary>

	public UIDraggableCamera draggableCamera;

	/// <summary>
	/// Automatically find the draggable WcCamera if possible.
	/// </summary>

	void Awake ()
	{
		if (draggableCamera == null)
		{
			draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(gameObject);
		}
	}

	/// <summary>
	/// Forward the press event to the draggable WcCamera.
	/// </summary>

	void OnPress (bool isPressed)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Press(isPressed);
		}
	}

	/// <summary>
	/// Forward the drag event to the draggable WcCamera.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Drag(delta);
		}
	}

	/// <summary>
	/// Forward the scroll event to the draggable WcCamera.
	/// </summary>

	void OnScroll (float delta)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Scroll(delta);
		}
	}
}
