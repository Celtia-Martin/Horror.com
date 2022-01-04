using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CargadorController : MonoBehaviour
{

    //referencias
    private InputAction activar;
    private InputAction respawn;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
    [SerializeField] private GameObject cargador;
    [SerializeField] private GameObject otroCargador;
    private MovilController movil;
    [SerializeField] private GameObject interfaz;
    [SerializeField] private GameObject otroMovil;
    [SerializeField] private Transform [] targets;

    //otros
    private bool cargando = false;
    private bool sePuedeCoger = false;
    private bool input = false;
    private void Start()
    {
        cargador.transform.position = targets[Random.Range(0,targets.Length)].position;
        activar = myActionsAsset.FindAction("XRI LeftHand/ActivarCargado"); 
        respawn = myActionsAsset.FindAction("XRI LeftHand/Respawn");
        respawn.performed += Respawn;
    }
    void Respawn(InputAction.CallbackContext obj)
    {
        cargador.transform.position = targets[Random.Range(0, targets.Length)].position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cargando)
        {
            if ( (other.gameObject.tag.Equals("Mano")))
            {
                interfaz.SetActive(true);
            }
        }
        else
        {
            if ((other.gameObject == cargador) )
            {
                interfaz.SetActive(true);
            }
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject == cargador) || other.gameObject.tag.Equals("Mano"))
        {
            interfaz.SetActive(false);
        }
    }
    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(2);
        sePuedeCoger = true;
    }
    private void Update()
    {
        input = activar.triggered;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == cargador)
        {

            if (input)
            {
                if (!cargando)
                {

                    StartCoroutine(coolDown());
                    cargando = true;
                    movil.aCargar(true);
                    otroMovil.SetActive(true);
                    cargador.SetActive(false);
                    otroCargador.SetActive(true);
                }
            }
        }
        else if (other.gameObject.tag.Equals("Mano"))
        {
            if (input)
            {
                if (sePuedeCoger)
                {
                    cargador.transform.position = targets[(int)Random.Range(0, targets.Length)].position;
                    cargando = false;
                    movil.aCargar(false);
                    otroMovil.SetActive(false);
                    cargador.SetActive(true);
                    otroCargador.SetActive(false);
                    sePuedeCoger = false;
                }
            }
        }
    }
    
    public void setMovil(MovilController movil)
    {
        this.movil = movil;
    }

}
