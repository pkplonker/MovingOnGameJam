using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Variable", menuName = "Variables/Int Variable")]
public class IntVariable : GameEvent
{
	[SerializeField]
	int value;

	public int Get() { return value; }
	public void Set(int val) { value = val; Raise(); }
}