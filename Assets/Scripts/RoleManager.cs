using UnityEngine;
using System.Collections;
using Ubiq.Messaging;

public class RoleManager : MonoBehaviour
{
    NetworkContext context;
    public static RoleManager Instance { get; private set; }

    public string role; //string for roles

    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Initialize isOwner here or elsewhere as needed
            role = "garbage";
        }
        else
        {
            Destroy(gameObject); // Ensures there is only one instance
        }
    }

    // Update is called once per frame
    public void UpdateRole(string new_role)
    {
        role = new_role;
    }
}
