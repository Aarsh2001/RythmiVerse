using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Logging;

public class PlayerCollision : MonoBehaviour
{
    NetworkContext context;
    ExperimentLogEmitter info;
<<<<<<< Updated upstream
=======

    

    
>>>>>>> Stashed changes
    // Define all tags 
    string[] tagsOfInterest = new string[] { "snare_tom", "floor_tom", "rack_tom1", "rack_tom2", "crash", "ride", "hi_hat" };
    private List<SoundMessage> collisionHistory = new List<SoundMessage>(); // Store collision history

    void Start() // Corrected from 'void start()'
    {    
    
       context = NetworkScene.Register(this);
<<<<<<< Updated upstream
       info = new ExperimentLogEmitter(this);
=======
>>>>>>> Stashed changes
    }

    public struct SoundMessage
    {
        public string tagOfHitObject;
        public float collisiontime;
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
                    var soundMessage = new SoundMessage { tagOfHitObject = tag,  collisiontime = Time.time};
                    context.SendJson(soundMessage);
                }
                break;
            }
        }
    }
    public List<SoundMessage> GetCollisionHistory()
    {
        return collisionHistory;
    }


    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {   
        var soundMessage = message.FromJson<SoundMessage>();
        collisionHistory.Add(soundMessage);
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
