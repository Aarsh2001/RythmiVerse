using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public PlayerCollision playerCollision; // Assign this in the Inspector

    // Trigger this method however you like (e.g., with a UI button press)
    public void TriggerReplay()
    {
       
        
            StartCoroutine(playerCollision.ReplayCollisionSequence(5f));
        
    }
}
