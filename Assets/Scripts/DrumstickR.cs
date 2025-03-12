using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;


public class DrumstickR : MonoBehaviour
{
    NetworkContext context;

    private DrumKitManager manager;

    // Start is called before the first frame update
    // void Start()
    // {
    //     context = NetworkScene.Register(this);
    //     manager = DrumKitManager.Instance;
    //     var grab = GetComponent<XRGrabInteractable>();
    //     grab.activated.AddListener(XRGrabInteractable_Activated);
    // }
    void Start()
    {
        context = NetworkScene.Register(this);
        manager = DrumKitManager.Instance;
        if (manager == null)
        {
            Debug.LogError("DrumKitManager instance not found.");
            return;
        }

        var grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.activated.AddListener(XRGrabInteractable_Activated);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component not found.");
        }
    }

    public void XRGrabInteractable_Activated(ActivateEventArgs eventArgs)
    {

        // Force the interactor(hand) to drop the firework
        var interactor = (XRBaseInteractor)eventArgs.interactorObject;
        interactor.allowSelect = false;
        var interactable = (XRGrabInteractable)eventArgs.interactableObject;
        interactable.enabled = false;
        interactor.allowSelect = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Only the owner should send updates
        if(manager.isOwner)
        {
           
            var message = new Message();
            message.pose = Transforms.ToLocal(transform, context.Scene.transform);
            context.SendJson(message);

        }
    }

    private struct Message
    {
        public Pose pose;
        // public int token; // Token for ownership logic
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();

        // Update the object only if the incoming token is higher
        var pose = Transforms.ToWorld(m.pose, context.Scene.transform);
        transform.position = pose.position;
        transform.rotation = pose.rotation;


    }
}
