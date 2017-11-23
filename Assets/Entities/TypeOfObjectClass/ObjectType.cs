// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour 
{
	protected Dictionary<Type, string> stringValues = new Dictionary<Type, string>();
	
	public void SetupStringValues()
	{
		stringValues.Add(Type.Tree, "Tree");
		stringValues.Add(Type.Rock, "Rock");
	}

	public string GetTypeStringValue(Type type)
	{
		string result = "";
		stringValues.TryGetValue(type, out result);

		return result;
	}

    public enum Type
    {
        Tree,
        Rock
    }
}
