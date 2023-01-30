using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;

    [SerializeField] private GameObject pickUp;
    [SerializeField] private GameObject deathEffect;

    public HealthBar healthBar;
    [SerializeField] private EnemyType enemyType;

    private PickupManager pickUpManager;

    [SerializeField] private Transform starSpawn;

    [SerializeField] private AudioClip hitSound;

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

    private void TakeDamage(float amount)
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            pickUpManager.RandomlyGenerateStar(Random.Range(0, 5), starSpawn.transform);
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            SoundManager.instance.PlayerSound(hitSound);
            enemyType.CorrectAnimation();
            TakeDamage(20f);
        }
    }
}
