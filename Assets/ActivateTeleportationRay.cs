using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject rightHandTeleportation;

    public InputActionProperty rightActivate;
    public InputActionProperty cancel;
    // Update is called once per frame
    void Update()
    {
        rightHandTeleportation.SetActive(cancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
