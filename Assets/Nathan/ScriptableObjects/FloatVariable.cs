using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Variable", menuName = "Variables/Float Variable")]
public class FloatVariable : GameEvent
{
	[SerializeField]
	float value;

	public float Get() { return value; }
	public void Set(float val) { value = val; Raise(); }
}