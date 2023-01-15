using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isFiring = false;
    public GameObject muzzleFlash;
    public Transform firePoint;

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.SetActive(true);

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        float shotDistance = 20f;

        if(Physics.Raycast(ray, out hit, shotDistance))
        {
            shotDistance = hit.distance;

        }

        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1f);
    }

    public void StopFiring()
    {
        isFiring = false;
        muzzleFlash.SetActive(false);
    }

    public float GunHeight
    {
        get{ return firePoint.position.y; }
    }
    
}
