using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public GameObject pickUp;
    public GameObject deathEffect;

    public HealthBar healthBar;
    private EnemyType enemyType;

    private PickupManager pickUpManager;

    private void Awake()
    {
        pickUpManager = PickupManager.instance;
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
            pickUpManager.RandomlyGenerateStar(Random.Range(0, 5), transform);
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
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
