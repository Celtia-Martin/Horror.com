using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovilController : MonoBehaviour
{
    [SerializeField]private Camera cFrontal;
    [SerializeField] private Camera cInterior;
    [SerializeField] private Image display;
    private bool movilActivado=true;
    private Texture2D pantalla;
    // Start is called before the first frame update
    void Start()
    {
        pantalla = new Texture2D(cFrontal.targetTexture.width, cFrontal.targetTexture.height);// aqui porque su asignacion en OnGui aumentaba la memoria ram demasiado rapido
    }


    private void OnGUI()
    {
        if (movilActivado)
        {
            Renderizado(cFrontal);
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
}
