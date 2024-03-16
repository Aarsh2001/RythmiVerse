using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumstickR : MonoBehaviour
{
    private Animator animator_R;

    // Start is called before the first frame update
    void Start()
    {
        animator_R = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator_R != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                animator_R.SetTrigger("hit");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator_R.SetTrigger("spin");
            }
        }
    }

}
