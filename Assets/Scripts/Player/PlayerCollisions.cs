using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject[] star;
    public Pickup justCollidedWith;

    private ScoreManager score;
    private PlayerHealth health;

    private Animator anim;
    public Animator textAnim;

    public GameObject floatingText;

    private TransitionManager transitions;

    public AudioClip coinCollect;

    private void Start()
    {
        transitions = TransitionManager.instance;
        score = ScoreManager.instance;
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Star")
        {
            //Get the pickup I am colliding with
            var stars = other.gameObject.GetComponent<Pickup>();
            if (justCollidedWith == null) return;

            //If the pickup is the one I have just collided with do nothing
            if (stars == justCollidedWith)
            {
                justCollidedWith = null;
                return;
            }

            //Play pickup animation and destroy
            stars.GetPickedUp();
            Instantiate(floatingText, other.transform.position, Quaternion.identity);
            score.scoreCount += 1;

            textAnim.SetTrigger("ScoreIncrease");
            textAnim.SetTrigger("HealthIncrease");

            health.currentHealth++;

            SoundManager.instance.PlayerSound(coinCollect);

            Destroy(other.gameObject, 0.5f);
        }

        if(other.gameObject.tag == "Door" && score.scoreCount >= score.scoreForLevel)
        {
            transitions.StartCoroutine(transitions.NextScene());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health.TakeDamage(1f);
            textAnim.SetTrigger("HealthDecrease");
            anim.SetTrigger("Hit");
        }
    }
}
