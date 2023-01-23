using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject[] star;
    public Pickup justCollidedWith;

    ScoreManager score;

    PlayerHealth health;

    Animator anim;

    public Animator textAnim;

    private void Start()
    {
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
            if(stars == justCollidedWith)
            {
                justCollidedWith = null;
                return;
            }

            //Play pickup animation and destroy
            stars.GetPickedUp();
            score.scoreCount += 1;
            textAnim.SetTrigger("ScoreBounce");
            health.currentHealth++;

            Destroy(other.gameObject, 0.5f);


        }

        if(other.gameObject.tag == "Door" && score.scoreCount >= score.scoreForLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health.TakeDamage(1f);

            anim.SetTrigger("Hit");
        }
    }
}
