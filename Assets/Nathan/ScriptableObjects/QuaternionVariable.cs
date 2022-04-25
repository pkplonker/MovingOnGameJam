using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quaternion Variable", menuName = "Variables/Quaternion Variable")]
public class QuaternionVariable : GameEvent
{
	[SerializeField]
	Quaternion value;

	public Quaternion Get() { return value; }
	public void Set(Quaternion val) { value = val; Raise(); }
}
