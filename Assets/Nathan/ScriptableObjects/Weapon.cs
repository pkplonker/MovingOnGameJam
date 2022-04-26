using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{

    public WeaponTemplate weaponTemplate;

    public int clipAmount;

    public int ammoAmount;
}
