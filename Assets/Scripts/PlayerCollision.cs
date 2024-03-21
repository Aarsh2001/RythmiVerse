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
        public float collisiontime;
    } 
    private List<SoundMessage> collisionHistory = new List<SoundMessage>(); // Store collision history
 
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
    public void PrintCollisionHistory()
{
    foreach (var soundMessage in collisionHistory)
    {
        Debug.Log($"Tag: {soundMessage.tagOfHitObject}, Collision Time: {soundMessage.collisiontime}");
    }
}

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)) // When P key is pressed
    {
        PrintCollisionHistory();
    }
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
