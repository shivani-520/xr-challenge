using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject[] star;
    public Pickup justCollidedWith;

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
            Destroy(other.gameObject, 1f);


        }
    }
}
