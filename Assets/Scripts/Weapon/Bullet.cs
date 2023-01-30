using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float life = 3f;

    [SerializeField] private AudioClip gunShot;

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
