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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        Texture2D pantalla = new Texture2D(target.targetTexture.width, target.targetTexture.height);
        pantalla.ReadPixels(new Rect(0, 0, target.targetTexture.width, target.targetTexture.height), 0, 0);
        pantalla.Apply();
        RenderTexture.active = current;
        display.sprite = Sprite.Create(pantalla, new Rect(0, 0, target.targetTexture.width, target.targetTexture.height), new Vector2(0, 0));
        
        
    }
}
