// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LockableObject))]
public class LockObject : Editor 
{	
	public override void OnInspectorGUI()
	{
		LockableObject obj = target as LockableObject;
		obj.locked = EditorGUILayout.Toggle("Locked", obj.locked);
		//obj.gameObject.hideFlags = obj.locked ? HideFlags.NotEditable : HideFlags.None;
		if (!obj.locked)
		{
			obj.gameObject.hideFlags = HideFlags.None;			
		}
		else
		{
			Component[] components = obj.GetComponentsInChildren<Component>();
			foreach (Component component in components)
			{
				if (!(component is LockableObject))
				{
					component.hideFlags = HideFlags.NotEditable;
				}
			}
		}
		DrawDefaultInspector();
	}
}
