using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPickup : MonoBehaviour
{
    public GameObject star;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Star")
        {
            Destroy(star);
        }
    }
}
