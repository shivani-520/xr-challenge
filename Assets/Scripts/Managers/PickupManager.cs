using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager instance;
    [SerializeField] private GameObject pickUp;

    [SerializeField] private Animator textAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        transform.localPosition = new Vector3(0, 5, 0);

    }


    public void RandomlyGenerateStar(int probability, Transform spawnPoint)
    {

        if (Random.Range(0, 1) < probability)
        {
            Instantiate(pickUp, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
}
