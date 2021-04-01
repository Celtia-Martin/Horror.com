using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteraccionesCaja : MonoBehaviour
{
 
    public void SetRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    
    }
    public void SetBlue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;

    }
}
