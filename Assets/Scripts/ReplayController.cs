using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Logging;


public class ReplayController : MonoBehaviour
{
    public PlayerCollision playerCollision; // Assign this in the inspector
    public float delayBeforeStart = 5f; // Time in seconds before the replay starts

    // Use Start or Awake to trigger the delayed replay
    void Start()
    {
        StartCoroutine(DelayedStartReplay(delayBeforeStart));
    }

    private IEnumerator DelayedStartReplay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        StartReplay(); // Then start the replay
    }

    private IEnumerator ReplaySequence()
    {
        // Ensure playerCollision is not null
        if (playerCollision == null)
        {
            Debug.LogError("PlayerCollision reference is not set in ReplayController.");
            yield break;
        }
        yield return new WaitForSeconds(40f); // Wait for the specified delay
        Debug.Log("Start sequence");
    
        List<PlayerCollision.SoundMessage> collisionHistory = playerCollision.GetCollisionHistory();
        Debug.Log(collisionHistory[0]);

        foreach (var soundMessage in collisionHistory)
        {
            Debug.Log(soundMessage);
            GameObject[] objs = GameObject.FindGameObjectsWithTag(soundMessage.tagOfHitObject);
            if (objs.Length > 0)
            {
                AudioSource aud = objs[0].GetComponent<AudioSource>();
                Animator animator = objs[0].GetComponent<Animator>();

                if (aud != null && animator != null)
                {
                    animator.SetTrigger("hit");
                    aud.Play();
                    // Wait for a bit before moving to the next sound message
                    yield return new WaitForSeconds(1); // Adjust wait time as needed
                }
            }
        }
    }

    public void StartReplay()
    {
        StartCoroutine(ReplaySequence());
    }
}
