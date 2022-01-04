using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Horror_com : MonoBehaviour
{
   
    //Referencias
    [SerializeField] private Text puntuacion;
    [SerializeField] private Image valorImage;
    [SerializeField] private MonsterIA monster;
    [SerializeField] private GameObject interfazMuerte;
    [SerializeField] private Text puntuacionMuerte;
    private InputAction Reiniciar;
    private InputAction Salir;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;

    //otros
    private const float maxValor=6;
    private const float minValor = 0;
    private int vidas = 2;
    private bool muerto = false;
    private bool asustando = false;
    private int currentPuntos = 0;
    private float currentValor = 10;

    private void Start()
    {
        FindObjectOfType<MedidorValor>().setHorrorCom(this);
        Reiniciar = myActionsAsset.FindAction("XRI RightHand/Jugar");
        Salir = myActionsAsset.FindAction("XRI RightHand/Salir");
        Salir.performed += Saliendo;
        Reiniciar.performed += ReiniciarJuego;
    }
    void ReiniciarJuego (InputAction.CallbackContext obj)
    {
        if (muerto)
        {
            Time.timeScale = 1;
            Reiniciar.performed -= ReiniciarJuego;
            SceneManager.LoadScene("Juego");
  
        }
      
    }
    void Saliendo(InputAction.CallbackContext obj)
    {
        if (muerto)
        {
            Application.Quit();
        }

    }
    public void ModificarPuntuacion(int puntos)
    {
        currentPuntos += puntos;
        puntuacion.text = currentPuntos.ToString();

    }
    public void Valor(float valor)
    {
       
        currentValor = Mathf.Min(valor + currentValor, maxValor);
        currentValor = Mathf.Max(currentValor, minValor);
        valorImage.fillAmount = currentValor / maxValor;
        if ((currentValor == minValor)&&(!asustando))
        {
            asustando = true;
            monster.setAsustando(true);
            StartCoroutine(esperarSusto());
        }
      
    }
    IEnumerator esperarSusto()
    {
        yield return new WaitUntil(()=>!monster.getAsustando());
        vidas--;     
        Debug.Log("Vidas: " + vidas);
        if (vidas <= 0)
        {
       
            interfazMuerte.SetActive(true);
            puntuacionMuerte.text = currentPuntos.ToString();
           
            muerto = true; 
            Time.timeScale = 0;
            //GameOver
        }
        asustando = false;
    }
}
