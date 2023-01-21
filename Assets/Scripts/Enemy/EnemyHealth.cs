using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public GameObject pickUp;

    public HealthBar healthBar;

    public GameObject deathEffect;

    private EnemyType enemyType;

    public Transform starSpawn;

    private void Awake()
    {
        enemyType = GetComponent<EnemyType>();
    }

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void TakeDamage(float amount)
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            RandomlyGenerateStar(Random.Range(0, 5));
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    void RandomlyGenerateStar(int probability)
    {
        if(Random.Range(0, 1) < probability)
        {
            Instantiate(pickUp, starSpawn.position, starSpawn.rotation);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyType.CorrectAnimation();
            TakeDamage(20f);
        }
    }
}
