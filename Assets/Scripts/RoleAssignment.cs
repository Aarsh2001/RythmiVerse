using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;

public class RoleAssignment : MonoBehaviour
{
    // This method can be directly called by the XR Interaction event
    NetworkContext context;
    private RoleManager manager;


    public void Start()
    {
        context = NetworkScene.Register(this);
        manager = RoleManager.Instance;

    }
    public void Update()
    {
        var message = new Message();
        if (gameObject.CompareTag("TeacherButton"))
        {
            AssignTeacherRole();
            manager.role = "teacher";
            context.SendJson(manager.role);
        }
        else if (gameObject.CompareTag("StudentButton"))
        {
            AssignStudentRole();
            manager.role = "teacher";
            context.SendJson(manager.role);
        }
    }
    private void AssignTeacherRole()
    {
        Debug.Log("Assigned as Teacher");
        // Additional teacher role assignRment logic here
    }

    private void AssignStudentRole()
    {
        Debug.Log("Assigned as Student");
        // Additional student role assignment logic here
    }
    private struct Message
    {
        public PositionRotation pose;
        // public int token; // Token for ownership logic
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = context.FromJson<Message>();

        if (m == "teacher){
            //deactivate grab

         }
       if (mn == "student"){
        // deactivate student grab
        }
       
}
