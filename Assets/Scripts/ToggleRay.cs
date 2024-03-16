using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleRay : MonoBehaviour
{
    public GameObject teleportationRay;
    public GameObject drumstick;
    public InputActionProperty rayActivate;
    void Update()
    {
        teleportationRay.SetActive(rayActivate.action.ReadValue<float>() > 0.1f);
        drumstick.SetActive(rayActivate.action.ReadValue<float>() < 0.1f);
    }
}
