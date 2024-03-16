using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManage : MonoBehaviour
{
    public GameObject teleportationRay_L;
    public GameObject teleportationRay_R;
    public GameObject drumstick_L;
    public GameObject drumstick_R;
    public InputActionReference toggleReference_L = null;
    public InputActionReference toggleReference_R = null;
    void Start()
    {
        
    }
    private void Awake()
    {
        toggleReference_L.action.started += ToggleL;
        toggleReference_R.action.started += ToggleR;
    }

    private void OnDestroy() 
    {
        toggleReference_L.action.started -= ToggleL;
        toggleReference_R.action.started -= ToggleR;
    }

    private void ToggleL(InputAction.CallbackContext context)
    {
        if (drumstick_L.activeSelf)
        {
            drumstick_L.SetActive(false);
            teleportationRay_L.SetActive(true);
        }
        else
        {
            drumstick_L.SetActive(true);
            teleportationRay_L.SetActive(false);
        }
    }

    private void ToggleR(InputAction.CallbackContext context)
    {
        if (drumstick_R.activeSelf)
        {
            drumstick_R.SetActive(false);
            teleportationRay_R.SetActive(true);
        }
        else
        {
            drumstick_R.SetActive(true);
            teleportationRay_R.SetActive(false);
        }
    }
}
