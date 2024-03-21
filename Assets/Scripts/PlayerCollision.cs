using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCollision : MonoBehaviour
{
    NetworkContext context;

    // Define all tags 
    string[] tagsOfInterest = new string[] { "snare_tom", "floor_tom", "rack_tom1", "rack_tom2", "crash", "ride", "hi_hat" };

    void Start() // Corrected from 'void start()'
    {
        context = NetworkScene.Register(this);
    }

    private struct SoundMessage
    {
        public string tagOfHitObject;
    }

    [System.Serializable] // This attribute makes the struct serializable
    public struct CollisionData
    {
        public string tagOfHitObject;
        public float collisionTime; // The time when the collision occurred
    }

    private List<CollisionData> collisionHistory = new List<CollisionData>(); // Store collision history
    [System.Serializable]
    public struct CollisionSequenceMessage
    {
        public List<CollisionData> collisionSequence;
    }
    public void SendCollisionSequence()
    {
        var sequenceMessage = new CollisionSequenceMessage { collisionSequence = collisionHistory };
        string jsonMessage = JsonUtility.ToJson(sequenceMessage);
        context.SendJson(jsonMessage); // Assuming SendText is a method that can send JSON strings over your network
    }

    void OnCollisionEnter(Collision collisionInfo)
    { 
        foreach (var tag in tagsOfInterest)
        {
            if (collisionInfo.gameObject.tag == tag)
            {   
                Animator animator = collisionInfo.gameObject.GetComponent<Animator>();
                AudioSource aud = collisionInfo.gameObject.GetComponent<AudioSource>();

                if (animator != null)
                {
                    animator.SetTrigger("hit");
                }
                if (aud != null)
                {
                    aud.Play();
                    var soundMessage = new SoundMessage { tagOfHitObject = tag };
                    collisionHistory.Add(new CollisionData { tagOfHitObject = tag, collisionTime = Time.time });
                    context.SendJson(soundMessage);
                }
                break;
            }
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {   
        var soundMessage = message.FromJson<SoundMessage>();
        Debug.Log($"Received sound trigger for tag: {soundMessage.tagOfHitObject}");
        // Debug.Log($"Received collision object: {soundMessage.info}");
        GameObject[] obj = GameObject.FindGameObjectsWithTag(soundMessage.tagOfHitObject);
        AudioSource aud = obj[0].GetComponent<AudioSource>();
        Animator animator = obj[0].GetComponent<Animator>();
        if (aud != null)
        {
            aud.Play();
        }
        if (animator != null)
        {
            animator.SetTrigger("hit");
        } 
    }

}
