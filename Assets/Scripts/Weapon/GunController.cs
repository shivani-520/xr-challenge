using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;

    private float nextShot;

    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform[] firePoint;

    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject bulletPrefab;

    private void Update()
    {
        if(isFiring)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    public void StartFiring()
    {
        isFiring = true;

        muzzleFlashPrefab.SetActive(true);

        if (Time.time > nextShot)
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

        muzzleFlashPrefab.SetActive(false);

    }
}
