using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Messaging;
using Ubiq.Geometry;

public class RoleAssignment : MonoBehaviour
{
    // This method can be directly called by the XR Interaction event
    NetworkContext context;

    private void Start()
    {
        context = NetworkContext.Register(this);
        ConnectionManagerEditor = RoleManager.Instance;

    }
    public void AssignRoleBasedOnXRInteractor(XRBaseInteractor interactor)
    {
        if (gameObject.CompareTag("TeacherButton"))
        {
            AssignTeacherRole();
        }
        else if (gameObject.CompareTag("StudentButton"))
        {
            AssignStudentRole();
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
}
