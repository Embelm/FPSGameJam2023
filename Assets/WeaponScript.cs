using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    enum WeaponType 
    {
        Rifle
    }

    public enum FireSelection
    {
        SemiAuto,
        FullAuto
    }

    enum GunState
    {
        Firing,
        Reloading
    }


    [SerializeField] WeaponType Weapon;
    public FireSelection fireSelect;

    public int gunDamage = 1;
    public float fireRate = .1f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;


    public Camera fpsCamera;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    public AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public bool canFire = true;
    public int maxAmmo;
    private int currentAmmo;

    [SerializeField] StarterAssets.StarterAssetsInputs playerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        Weapon = WeaponType.Rifle;
        fireSelect = FireSelection.SemiAuto;
        laserLine = GetComponent<LineRenderer>();
        maxAmmo = currentAmmo;

    }
    private void Update()
    {
        
    }

    public void WeaponUpdate(FireSelection fireRateType)
    {
        FireRateUpdate(fireRateType);
    }


    private IEnumerator ShotEffect()
    {
        gunAudio.PlayOneShot(gunAudio.clip);

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    private void FireRateUpdate(FireSelection _fireSelection)
    {
        AmmoHandler();
        if(currentAmmo > 0)
        {
            if (_fireSelection == FireSelection.FullAuto)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;

                    LaserRaycast();
                }
            }
            else if (_fireSelection == FireSelection.SemiAuto)
            {
                if (canFire)
                {
                    LaserRaycast();
                    canFire = false;
                }
            }
        }
        

    }
    private void LaserRaycast()
    {
        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);
            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                EnemyHealthComponent enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealthComponent>();
                enemyHealth.DoDamage(1);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * weaponRange));
        }
    }

    private void AmmoHandler()
    {
        if (currentAmmo <= 0)
        {
            // gun is empty
        }
        else
        {
            currentAmmo--;
        }

    }

}
