using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager instance;
    public GameObject pickUp;

    public Animator textAnim;

    private void Awake()
    {
        instance = this;


    }


    public void RandomlyGenerateStar(int probability, Transform spawnPoint)
    {

        if (Random.Range(0, 1) < probability)
        {
            Instantiate(pickUp, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            return;
        }
    }
}
