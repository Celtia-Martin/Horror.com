using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CajaInteractuable : MonoBehaviour
{
    private InputAction myAction;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
    private Vector3 initialPosition;//Secondary Button Right
    private void Start()
    {
        initialPosition = transform.position;
        myAction = myActionsAsset.FindAction("XRI LeftHand/RecuperarCaja");
    }
    private void Update()
    {
        if (myAction.triggered)
        {
            transform.position = initialPosition;
        }
    }
}
