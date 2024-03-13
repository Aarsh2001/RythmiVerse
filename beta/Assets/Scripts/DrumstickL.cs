using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumstickL : MonoBehaviour
{
    private Animator animator_L;

    // Start is called before the first frame update
    void Start()
    {
        animator_L = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator_L != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator_L.SetTrigger("hit");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator_L.SetTrigger("spin");
            }
        }
    }
}
