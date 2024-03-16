using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGrab : MonoBehaviour
{
    private Collider cld;
    public GameObject drumstick_R;
    void Start()
    {
        cld = GetComponent<Collider>();
    }

    void Update()
    {
       if (drumstick_R.activeSelf)
       {
            cld.enabled = false;
       }
       else
       {
            cld.enabled = true;
       }
    }
}
