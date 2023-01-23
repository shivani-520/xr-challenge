using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Assign Health")]
    public float maxHealth;
    public float currentHealth;

    public TMP_Text healthText;

    public GameObject playerDeathEffect;

    public Transform deathSpawn;

    public Animator textAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        healthText.text = currentHealth.ToString("HEALTH 0");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameObject effect = Instantiate(playerDeathEffect, deathSpawn.position, deathSpawn.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject, 0.1f);
        }
    }

}
