using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teletransporte : MonoBehaviour
{
    //El eje vertical es el Y
    [SerializeField] private GameObject prefabCilindro;
    private GameObject currentCilindro;
    private float spawn = 3;
    private float speed = 0.01f;
    private bool activado = false;

    private InputAction activarTeletransporte;
    private InputAction movimientoTeletransporte;
    private InputAction confirmarTeletransporte;

   
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
  
    void Start()
    {
        activarTeletransporte = myActionsAsset.FindAction("XRI RightHand/ActivarTele");
        
        movimientoTeletransporte = myActionsAsset.FindAction("XRI RightHand/TecladoMoverTele");
        confirmarTeletransporte = myActionsAsset.FindAction("XRI RightHand/ConfirmarTele");
    }

   private void Activar()
    {
        activado = true;
        currentCilindro = Instantiate(prefabCilindro);
        Vector3 position= transform.forward*spawn+transform.position;
        position.y = -0.8f;
        currentCilindro.transform.position = position;


    }
    private void DesActivar()
    {
        activado = false;
        Destroy(currentCilindro);
        currentCilindro = null;
    }
    private void MoverCilindro(Vector2 Axis)
    {
        Vector3 oldPosition = currentCilindro.transform.position;
        // currentCilindro.transform.localPosition = new Vector3(oldPosition.x + Axis.y*speed, oldPosition.y, oldPosition.z + Axis.x*speed);
        Vector3 newPosition = oldPosition + (Axis.y * transform.forward * speed) + (Axis.x * transform.right * speed);
        currentCilindro.transform.localPosition = newPosition;
       
    }
    private void Teletransportarse()
    {
        Vector3 target = currentCilindro.transform.position;
        transform.position = new Vector3(target.x, transform.position.y, target.z);
    }

    private void Update()
    {
        if (activado)
        {
            MoverCilindro(movimientoTeletransporte.ReadValue<Vector2>());
            if (activarTeletransporte.triggered)
            {
                DesActivar();
            }
            if (confirmarTeletransporte.triggered)
            {
                Teletransportarse();
                DesActivar();
            }
            
        }
        else
        {
            if (activarTeletransporte.triggered)
            {

                Activar();
            }
        }
    }

}
