using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public GameObject pickUp;

    public HealthBar healthBar;

    Animator anim;

    public GameObject deathEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            RandomlyGenerateStar(Random.Range(0, 10));
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    void RandomlyGenerateStar(int probability)
    {
        if(Random.Range(0, 10) < probability)
        {
            Instantiate(pickUp, transform.position, transform.rotation);
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
            anim.SetTrigger("Hit");
            TakeDamage(20f);
        }
    }
}
