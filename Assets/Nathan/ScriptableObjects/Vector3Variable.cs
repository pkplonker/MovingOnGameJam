using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3 Variable", menuName = "Variables/Vector3 Variable")]
public class Vector3Variable : GameEvent
{
	[SerializeField]
	Vector3 value;

	public Vector3 Get() { return value; }
	public void Set(Vector3 val) { value = val; Raise(); }
}
