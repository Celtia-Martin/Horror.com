using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSalirManager : MonoBehaviour
{
    //Referencias
    private InputAction salir;
    private InputAction opciones;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
    [SerializeField] private GameObject interfaz;
    [SerializeField] private Image ultimaFoto;
    private MovilController movil;

    //otros
    private bool activado = false;
    void Start()
    {
        opciones = myActionsAsset.FindAction("XRI RightHand/Opciones");
        salir = myActionsAsset.FindAction("XRI RightHand/Salir");
        opciones.performed += OpcionesAction;
        salir.performed += SalirAction;
    }
    public void setMovil(MovilController movil)
    {
        this.movil = movil;
    }
    void SalirAction(InputAction.CallbackContext obj)
    {
        if (activado)
        {
            Application.Quit();
        }

    }
    void OpcionesAction(InputAction.CallbackContext obj)
    {
        activado = !activado;
        interfaz.SetActive(activado);
        if (activado)
        {
            Texture2D ultima = movil.ultimaFoto;
            if (ultima != null)
            {
                ultimaFoto.sprite = Sprite.Create(ultima, new Rect(0, 0, ultima.width, ultima.height), new Vector2(0, 0));

            }

        }
    }
}
