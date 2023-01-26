using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;

    public AudioClip gunShot;

    private void Awake()
    {
        SoundManager.instance.PlayerSound(gunShot);

        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
