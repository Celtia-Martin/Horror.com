using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR.Interaction.Toolkit;

public class Movimiento : MonoBehaviour
{
    //Referencias


    [SerializeField] private Transform camera;
    private InputAction movimiento;
    private AudioSource myAudio;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
    [SerializeField] private PostProcessLayer efecto;
    private Rigidbody myBody;
    [SerializeField] private GameObject Monstruo;

    //Propiedades

    private float speed = 0.035f;
    private float epsilon = 0.08f;

  
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        movimiento = myActionsAsset.FindAction("XRI RightHand/Moverse");
        myBody = GetComponent<Rigidbody>();
       
        //Ignorar algunas colisiones para evitar comportamientos extraños

        XRGrabInteractable [] interactables= FindObjectsOfType<XRGrabInteractable>();
        foreach(XRGrabInteractable G in interactables)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), G.gameObject.GetComponent<Collider>());
        }
        Collider[] monstruoCollider = Monstruo.GetComponents<Collider>();
        foreach (Collider c in monstruoCollider)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), c);
        }

    }

    private void Moverse(Vector2 Axis) //Método de locomoción
    {
        if(Mathf.Abs(Axis.x)>epsilon || Mathf.Abs(Axis.y) > epsilon)
        {
            //Sonido Pasos
            if (!myAudio.isPlaying)
            {
                myAudio.Play();

            }

            efecto.enabled = true;
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = oldPosition + (Axis.y * camera.forward * speed) + (Axis.x * camera.right * speed);
            myBody.MovePosition(new Vector3(newPosition.x, transform.position.y, newPosition.z));
      
        }
        else
        {
            if (myAudio.isPlaying)
            {
                myAudio.Pause();

            }
            efecto.enabled = false;
        }
        

    }
    private void Update()
    {     
            Moverse(movimiento.ReadValue<Vector2>());   
    }

}
