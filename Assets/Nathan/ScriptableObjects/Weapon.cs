using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponFireMode
{
    eManual,
    eSemiAutomatic,
    eAutomatic
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;

    public WeaponFireMode weaponFireMode;

    public float weaponRateOfFire;

    public float weaponManualFireDelay;

    public float weapomSemiAutoDelay;

    public int weaponSemiAutoBurstAmount;

    public int weaponClipSize;

    public float weaponClipReloadTime;

    public Vector2 bulletSpreadAngle;

    public Stuart.Scripts.SO.ProjectileData bulletData;
}
