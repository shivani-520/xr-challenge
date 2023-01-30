using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyType;

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float timeBetweenWaves = 3f;

    private bool waveIsDone = true;

    // Update is called once per frame
    void Update()
    {
        if (waveIsDone == true && gameObject != null)
        {
            StartCoroutine(EnemySpawn());
        } else { return; }
    }

    IEnumerator EnemySpawn()
    {
        waveIsDone = false;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyType[Random.Range(0, enemyType.Length)], spawnPoints[i].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate -= 0.1f;

        yield return new WaitForSeconds(timeBetweenWaves);

        waveIsDone = true;
    }
}
