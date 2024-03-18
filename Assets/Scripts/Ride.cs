using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class Ride : MonoBehaviour
{
    NetworkContext context;

    public int token; // Token for deciding ownership
    public bool isOwner; // Flag to indicate if this instance is the owner

    Vector3 lastPosition;
    Quaternion lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000); // Assign a random token
        isOwner = true; // Assume ownership initially
    }

    // Update is called once per frame
    void Update()
    {
        // Only the owner should send updates
        if(isOwner && (lastPosition != transform.localPosition || lastRotation != transform.localRotation))
        {
            lastPosition = transform.localPosition;
            lastRotation = transform.localRotation;

            context.SendJson(new Message()
            {
                position = transform.localPosition,
                rotation = transform.localRotation,
                token = token // Include the token in the message
            });
        }
    }

    private struct Message
    {
        public Vector3 position;
        public Quaternion rotation;
        public int token; // Token for ownership logic
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();

        // Update the object only if the incoming token is higher
        if(m.token > token)
        {
            transform.localPosition = m.position;
            transform.localRotation = m.rotation;

            // Make sure the logic in Update doesn't trigger as a result of this update
            lastPosition = transform.localPosition;
            lastRotation = transform.localRotation;

            // Update token and ownership
            token = m.token;
            isOwner = false;
        }
    }

    // Implement logic for taking ownership (e.g., when interacting with the object)
    public void TakeOwnership()
    {
        token++;
        isOwner = true;
    }
}
