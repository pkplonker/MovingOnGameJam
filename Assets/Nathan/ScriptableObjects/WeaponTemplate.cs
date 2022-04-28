using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponFireMode
{
    eManual,
    eSemiAutomatic,
    eAutomatic
}

[CreateAssetMenu(fileName = "Weapon Template", menuName = "Weapons/Weapon Template")]
public class WeaponTemplate : ScriptableObject
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

    public GameObject weaponModelPrefab;

    public Stuart.Scripts.SO.ProjectileData bulletData;
}
