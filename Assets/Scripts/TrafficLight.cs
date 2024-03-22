using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TrafficLight : MonoBehaviour
{
    public bool isTeacher = true;
    public InputActionReference toggleReference = null;
    public GameObject drumstikcR;
    private GameObject teacherBaseDrum;



    void Start()
    {
        teacherBaseDrum = FindObjectByTagAndLayer("base_drum", "teacher_drum");
    }

    GameObject FindObjectByTagAndLayer(string tag, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.layer == layer)
            {
                return obj; // Return the first found object
            }
        }
        return null; // Return null if no object found
    }








    void OnCollisionEnter(Collision collisionInfo)
    {
        List<string> drumPartTags = new List<string>
        {
            "snare_tom",
            "floor_tom",
            "rack_tom1",
            "rack_tom2",
            "crash",
            "ride",
            "hi_hat"
        };

        foreach (var tag in drumPartTags)
        {
            if (collisionInfo.gameObject.tag == tag && collisionInfo.gameObject.layer == LayerMask.NameToLayer("teacher_drum"))
            {
                CreateAndModifyClone(collisionInfo.gameObject);
            }
        }
    }



    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy() 
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        
        if (drumstikcR.activeSelf && isTeacher)
        {
            //Debug.Log("base drum !");
            CreateAndModifyClone(teacherBaseDrum);
        }
    }







    private void CreateAndModifyClone(GameObject original)
    {
        GameObject clone = Instantiate(original, original.transform.position, original.transform.rotation);
        clone.transform.SetParent(original.transform.parent, true);
        clone.transform.localScale = original.transform.localScale;
        clone.tag = "clone";

        DisableComponents(clone);

        // Find destination object with the same tag
        GameObject destination = FindDestinationObject(original);
        
        if (destination != null)
        {
            // Adjust this call to pass the destination GameObject
            float moveSpeed = 1.0f; // Adjust this speed as necessary
            StartCoroutine(MoveObjectToDestination(clone, destination, moveSpeed));
        }
    }

    private GameObject FindDestinationObject(GameObject original)
    {
        GameObject[] allObjectsWithSameTag = GameObject.FindGameObjectsWithTag(original.tag);
        foreach (var obj in allObjectsWithSameTag)
        {
            if (obj != original && Vector3.Distance(obj.transform.position, original.transform.position) > 0.01f)
            {
                return obj; // Return the first object found that is not the original
            }
        }
        return null; // No valid destination found
    }

    IEnumerator MoveObjectToDestination(GameObject objectToMove, GameObject destinationObject, float speed)
    {
        // Initial color (orange) and final color (green)
        Color startColor = new Color(1f, 0.5f, 0.0f, 0.3f); // Orange with transparency
        Color endColor = new Color(0f, 1f, 0.0f, 0.3f); // Green with the same transparency

        Vector3 startPosition = objectToMove.transform.position;
        Quaternion startRotation = objectToMove.transform.rotation; // Store the starting rotation
        MeshRenderer renderer = objectToMove.GetComponent<MeshRenderer>();
        if (renderer == null) yield break;

        renderer.material.color = startColor;

        while (Vector3.Distance(objectToMove.transform.position, destinationObject.transform.position) > 0.1f)
        {
            // Use the destination object's current position and rotation
            Vector3 destinationPosition = destinationObject.transform.position;
            Quaternion destinationRotation = destinationObject.transform.rotation;

            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, destinationPosition, speed * Time.deltaTime);
            
            // Rotation interpolation (using Slerp for smoother rotation)
            float rotationSpeed = speed * 0.5f; // Adjust rotation speed as necessary. It's independent of movement speed.
            objectToMove.transform.rotation = Quaternion.Slerp(objectToMove.transform.rotation, destinationRotation, rotationSpeed * Time.deltaTime);

            // Recalculate total distance every loop iteration if the destination moves
            float totalDistance = Vector3.Distance(startPosition, destinationPosition);
            float distanceCovered = Vector3.Distance(startPosition, objectToMove.transform.position);
            float fractionOfJourney = distanceCovered / totalDistance;

            renderer.material.color = Color.Lerp(startColor, endColor, fractionOfJourney);

            yield return null;
        }

        Destroy(objectToMove);
    }

    private void DisableComponents(GameObject clone)
    {
        // Collect all MonoBehaviour components to remove
        List<MonoBehaviour> scripts = new List<MonoBehaviour>(clone.GetComponents<MonoBehaviour>());
        foreach (MonoBehaviour script in scripts)
        {
            Destroy(script); // Remove the script component
        }

        // Disable or modify other components as before
        foreach (Component component in clone.GetComponents<Component>())
        {
            if (!(component is MeshRenderer) && !(component is MonoBehaviour)) // Exclude MonoBehaviour since they are handled above
            {
                if (component is Behaviour)
                {
                    ((Behaviour)component).enabled = false;
                }
                else if (component is Collider)
                {
                    ((Collider)component).enabled = false;
                }
                else if (component is Rigidbody)
                {
                    Rigidbody rb = component as Rigidbody;
                    rb.isKinematic = true;
                    rb.detectCollisions = false;
                }
            }
        }

        ChangeColorToOrangeAndTransparent(clone);
    }

    private void ChangeColorToOrangeAndTransparent(GameObject clone)
    {
        MeshRenderer renderer = clone.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = 3000;
            renderer.material.color = new Color(1f, 0.5f, 0.0f, 0.3f);
        }
    }
}