using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{

    [SerializeField] InputActionReference actionGrip;
    [SerializeField] InputActionReference actionTrigger;
    private Animator handAnimator;
 
    void Awake()
    {
        actionGrip.action.performed += GripPress;
        actionTrigger.action.performed += Triggered;
        handAnimator = GetComponent<Animator>();
    }
    private void GripPress(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
        float b= obj.ReadValue<float>();
    }
    private void Triggered(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }
}
