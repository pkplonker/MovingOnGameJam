using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        var script = (Weapon)target;

        script.weaponName = EditorGUILayout.TextField("Weapon Name", script.weaponName);

        script.weaponClipSize = EditorGUILayout.IntField("Weapon Clip Size", script.weaponClipSize);

        script.weaponClipReloadTime = EditorGUILayout.FloatField("Weapon Clip Reload Time", script.weaponClipReloadTime);

        EditorGUILayout.Space();

        script.weaponFireMode = (WeaponFireMode)EditorGUILayout.EnumPopup("Weapon Fire Mode", script.weaponFireMode);

        if (script.weaponFireMode == WeaponFireMode.eManual)
        {
            script.weaponRateOfFire = EditorGUILayout.FloatField("Manual Rate Of Fire", script.weaponRateOfFire);
        }
        else if (script.weaponFireMode == WeaponFireMode.eSemiAutomatic)
        {
            script.weaponSemiAutoBurstAmount = EditorGUILayout.IntField("Semi-Auto Burst Amount:", script.weaponSemiAutoBurstAmount);
            script.weapomSemiAutoDelay = EditorGUILayout.FloatField("Semi-Auto Burst Rate Of Fire:", script.weapomSemiAutoDelay);
            script.weaponRateOfFire = EditorGUILayout.FloatField("Semi-Auto Rate Of Fire", script.weaponRateOfFire);
        }
        else if (script.weaponFireMode == WeaponFireMode.eAutomatic)
        {
            script.weaponRateOfFire = EditorGUILayout.FloatField("Automatic Rate Of Fire", script.weaponRateOfFire);
        }

        EditorGUILayout.Space();



        EditorGUILayout.Space();

        script.bulletData = (Stuart.Scripts.SO.ProjectileData)EditorGUILayout.ObjectField("Bullet Data", script.bulletData, typeof(Stuart.Scripts.SO.ProjectileData), false);
    }
}
