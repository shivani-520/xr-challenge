using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring = false;
    public GameObject muzzleFlash;

    public float fireRate;

    public Transform[] firePoint;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    float nextShot;

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.SetActive(true);

        if(Time.time > nextShot)
        {
            for (int i = 0; i < firePoint.Length; i++)
            {
                nextShot = Time.time + fireRate;

                GameObject bullet = Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);
                bullet.GetComponent<Rigidbody>().velocity = firePoint[i].forward * bulletSpeed;
            }
        }

    }

    public void StopFiring()
    {
        isFiring = false;
        muzzleFlash.SetActive(false);

    }

    
}
