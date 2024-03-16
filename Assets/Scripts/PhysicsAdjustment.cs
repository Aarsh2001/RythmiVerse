using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAdjustment : MonoBehaviour
{
    public Transform target;
    private Rigidbody rgbd;
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rgbd.velocity =  (target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotation_df = target.rotation * Quaternion.Inverse(transform.rotation);
        rotation_df.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotation_df_degree = angleInDegree * rotationAxis;
        rgbd.angularVelocity = (rotation_df_degree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
