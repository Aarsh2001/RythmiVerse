using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Animator animator;
    AudioSource aud;

    void OnCollisionEnter(Collision collisionInfo) 
    {
        if (collisionInfo.gameObject.tag == "snare_tom")
        {
            //Debug.Log("1");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "floor_tom")
        {
            //Debug.Log("2");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "rack_tom1")
        {
            //Debug.Log("3");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "rack_tom2")
        {
            //Debug.Log("4");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "ride_symbal1")
        {
            //Debug.Log("5");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "ride_symbal2")
        {
            //Debug.Log("6");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

        if (collisionInfo.gameObject.tag == "hi_hat")
        {
            //Debug.Log("7");
            animator = collisionInfo.gameObject.GetComponent<Animator>();
            aud = collisionInfo.gameObject.GetComponent<AudioSource>();
            if (animator != null) 
            {
                animator.SetTrigger("hit");
            }
            if (aud != null)
            {
                aud.Play();
            }
        }

    }
}
