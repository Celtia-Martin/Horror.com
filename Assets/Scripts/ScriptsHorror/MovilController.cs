using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovilController : MonoBehaviour
{


    //referencia
    private FieldOfView myfield;
    private Horror_com myhorror;
    private AudioSource myAudio;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject apagado;
    [SerializeField] private GameObject linterna;
    [SerializeField] private Camera cFrontal;
    [SerializeField] private Camera cInterior;
    [SerializeField] private Image display;
    [SerializeField] private Image bateryImage;
    [SerializeField] private Sprite[] bateryStages;
    [SerializeField] private GameObject[] ocultarCargando;
    private InputAction hacerFoto;
    private InputAction apagarLinterna;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;


    //Otros

    public Texture2D ultimaFoto;
    private Texture2D pantalla;

    private int Bateria = 100;
    private const int minBateria = 0;
    private const int maxBateria = 100;

    private bool movilActivado = true;
    private bool coolDown = true;
    private bool movilCargando = false;

    void Start()
    {
     
        FindObjectOfType<CargadorController>().setMovil(this);
        FindObjectOfType<MenuSalirManager>().setMovil(this);
        myhorror = GetComponent<Horror_com>();
        myAudio = GetComponent<AudioSource>();
        hacerFoto = myActionsAsset.FindAction("XRI RightHand/HacerFoto");
        apagarLinterna = myActionsAsset.FindAction("XRI RightHand/ApagarLinterna");
        myfield = GetComponent<FieldOfView>();
        pantalla = new Texture2D(cFrontal.targetTexture.width, cFrontal.targetTexture.height);
        ultimaFoto = new Texture2D(pantalla.width, pantalla.height);
        hacerFoto.performed += HacerFoto;
        apagarLinterna.performed += ApagarLinterna;
        StartCoroutine(BajadaBateria());
        StartCoroutine(SubidaBateria());
    }

    void ApagarLinterna(InputAction.CallbackContext obj)
    {
        if (!movilCargando)
        {
            linterna.SetActive(!linterna.activeSelf);
        }
        
    }
    void HacerFoto(InputAction.CallbackContext obj)
    {
        if ((movilActivado) && (!movilCargando))
        {
            if (coolDown)
            {

                float porcentaje = myfield.GetPuntuacion();
                Debug.Log("Foto! con " + porcentaje + " porcentaje");

                myhorror.ModificarPuntuacion(Mathf.RoundToInt(100 - (100 * Mathf.Abs(porcentaje))));
                myAudio.Play();
                StartCoroutine(EfectoShot());

                Graphics.CopyTexture(pantalla, ultimaFoto);

            }
        }

    }
    private void OnGUI()
    {
        if (movilActivado)
        {
            apagado.SetActive(false);
            Renderizado(cFrontal);
        }
        else
        {
            apagado.SetActive(true);
        }
    }
    public void aCargar(bool value)
    {
        movilCargando = value;
        movilActivado = !value;
        for( int i=0; i < ocultarCargando.Length; i++)
        {
            ocultarCargando[i].SetActive(!value);
        }
    }
    void Renderizado(Camera target)
    {
        var current = RenderTexture.active;
        RenderTexture.active = target.targetTexture;
        target.Render();
        pantalla.ReadPixels(new Rect(0, 0, target.targetTexture.width, target.targetTexture.height), 0, 0);
        pantalla.Apply();
        RenderTexture.active = current;
        display.sprite = Sprite.Create(pantalla, new Rect(0, 0, target.targetTexture.width, target.targetTexture.height), new Vector2(0, 0));
       
    }
    IEnumerator EfectoShot()
    {
        coolDown = false;
        shot.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        shot.SetActive(false);
        coolDown = true;
    }
    IEnumerator BajadaBateria()
    {
        while (true)
        {
            yield return new WaitUntil(() => !movilCargando);
            yield return new WaitForSeconds(1);
            Bateria = Mathf.Max(Bateria - 1, minBateria);
            //cambiar icono
            UpdateBateria(Bateria);
        }
    }
    IEnumerator SubidaBateria()
    {
        while (true)
        {
            yield return new WaitUntil(() => movilCargando);
            yield return new WaitForSeconds(0.1f);
            Bateria = Mathf.Min(Bateria + 1, maxBateria);
            UpdateBateria(Bateria);
        }
      
       
    }
    void UpdateBateria(int value)
    {
        if (value ==90)
        {
            bateryImage.sprite = bateryStages[0];
        }
        else if (value == 75)
        {
            bateryImage.sprite = bateryStages[1];
        }
        else if (value == 50)
        {
            bateryImage.sprite = bateryStages[2];
        }
        else if (value == 25)
        {
            bateryImage.sprite = bateryStages[3];

        }
        if (value == minBateria)
        {
            movilActivado = false;
            bateryImage.sprite = bateryStages[4];
        }
        else
        {
            movilActivado = true;
        }

    }

}
