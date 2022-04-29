using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<Weapon> selectableWeapon;

    List<KeyCode> weaponSelect = new List<KeyCode>{
        KeyCode.Alpha1, 
        KeyCode.Alpha2
    };


    [SerializeField]
    IntVariable selectedWeaponClip;

    Weapon selectedWeapon;

    Stuart.Scripts.Projectiles.ProjectileSpawner projectileSpawnerScript;

    //PlayerWeaponListVariable

    MeshRenderer mr;
    MeshFilter mf;

    GameObject weaponModel;

    float fireTimer;

    bool canFire;

    bool burstFireDone;

    int burstFireCount;

    public Transform crosshairTarget;

    void Start()
    {
        projectileSpawnerScript = GetComponent<Stuart.Scripts.Projectiles.ProjectileSpawner>();
        selectedWeapon = selectableWeapon[0];
        selectedWeapon.clipAmount = selectedWeapon.weaponTemplate.weaponClipSize;

        weaponModel = Instantiate(selectedWeapon.weaponTemplate.weaponModelPrefab, transform, false);

        burstFireDone = true;

    }

    // Update is called once per frame
    void Update()
    {
       
        bool clipHasAmmo = selectedWeapon.clipAmount > 0;
        bool fireTimerDone = fireTimer <= 0f;
        bool canReload = fireTimerDone && burstFireDone;

        canFire = fireTimerDone && clipHasAmmo && burstFireDone;

        int keyCount = 0;
        foreach (var key in weaponSelect)
        {
            if (Input.GetKeyDown(key) && (selectableWeapon.Count - 1 >= keyCount) && canReload)
            {
                selectedWeapon = selectableWeapon[keyCount];
                Destroy(weaponModel);
                weaponModel = Instantiate(selectedWeapon.weaponTemplate.weaponModelPrefab, transform, false);

            }

            keyCount++;
        }
      

        if (Input.GetMouseButton(0) && canFire)
        {
            if (selectedWeapon.weaponTemplate.weaponFireMode == WeaponFireMode.eManual && Input.GetMouseButtonDown(0))
            {
                fireTimer += selectedWeapon.weaponTemplate.weaponRateOfFire;
                GenerateBullet();


                selectedWeapon.clipAmount -= 1;
            }
            else if (selectedWeapon.weaponTemplate.weaponFireMode == WeaponFireMode.eSemiAutomatic)
            {
                burstFireDone = false;
                burstFireCount = 0;


                selectedWeapon.clipAmount -= 1;
            }
            else if (selectedWeapon.weaponTemplate.weaponFireMode == WeaponFireMode.eAutomatic)
            {
                fireTimer += selectedWeapon.weaponTemplate.weaponRateOfFire;
                GenerateBullet();


                selectedWeapon.clipAmount -= 1;
            }
        }
        else if (!burstFireDone)
        {
            bool firedBurstAmount = burstFireCount == selectedWeapon.weaponTemplate.weaponSemiAutoBurstAmount;
            bool canBurstFire = fireTimerDone && !firedBurstAmount && clipHasAmmo;

            if (canBurstFire)
            {
                fireTimer += selectedWeapon.weaponTemplate.weapomSemiAutoDelay;
                burstFireCount += 1;
                GenerateBullet();
            }
            else if (firedBurstAmount || !clipHasAmmo)
            {
                burstFireDone = true;
                fireTimer += selectedWeapon.weaponTemplate.weaponRateOfFire;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && canReload)
        {
            fireTimer += selectedWeapon.weaponTemplate.weaponClipReloadTime;
            selectedWeapon.clipAmount = selectedWeapon.weaponTemplate.weaponClipSize;
        }

        if (!fireTimerDone) fireTimer -= Time.deltaTime;

        if (selectedWeaponClip.Get() != selectedWeapon.clipAmount) selectedWeaponClip.Set(selectedWeapon.clipAmount);
    }


    void GenerateBullet()
    {
        if(selectedWeapon.weaponTemplate.bulletSpreadShot)
        { 
            for (int i = 0; i < selectedWeapon.weaponTemplate.bulletSpreadAmount; i++)
            {
                Vector3 bulletDirection = transform.forward;

                bulletDirection = Quaternion.Euler(
                    Random.Range(
                        -selectedWeapon.weaponTemplate.bulletSpreadAngle.x,
                        selectedWeapon.weaponTemplate.bulletSpreadAngle.x
                        ),
                    Random.Range(
                        -selectedWeapon.weaponTemplate.bulletSpreadAngle.y,
                        selectedWeapon.weaponTemplate.bulletSpreadAngle.y
                        ),
                    Random.Range(
                        -selectedWeapon.weaponTemplate.bulletSpreadAngle.z,
                        selectedWeapon.weaponTemplate.bulletSpreadAngle.z
                        )
                    ) * bulletDirection;

                Debug.Log(bulletDirection.normalized);

                projectileSpawnerScript.SpawnProjectile(transform.position, bulletDirection.normalized, selectedWeapon.weaponTemplate.bulletData, 0);
            }
        }
        else
        {
            projectileSpawnerScript.SpawnProjectile(transform.position, transform.forward, selectedWeapon.weaponTemplate.bulletData, 0);
        }
    }

    void FixedUpdate()
    {
        
    }
}
