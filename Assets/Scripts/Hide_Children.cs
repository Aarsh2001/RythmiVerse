using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Children : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in gameObject.transform) {
            t.gameObject.SetActive(false); // if you want to disable all
            //t.gameObject.SetActive(t.gameObject == objectToShow); // hide all, except required one
            // t.gameObject.SetActive(t == objectToShow.transform); // this also works
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
