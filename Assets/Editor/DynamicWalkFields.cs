// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CharacterBehaviour
{
	[CustomEditor(typeof(Walk))]
	public class DynamicWalkFields : Editor 
	{
		/* public override void OnInspectorGUI()
		{
			serializedObject.Update();
			Walk walk = target as Walk;
			walk.walkToWaypoint = GUILayout.Toggle(walk.walkToWaypoint, "Walk to waypoint");
			walk.walkInDirection = GUILayout.Toggle(walk.walkInDirection, "Walk in direction");

			if (walk.walkInDirection)
			{
				walk.direction = EditorGUILayout.Vector3Field("Direction in world space", walk.direction);
			}
		} */
	}
}