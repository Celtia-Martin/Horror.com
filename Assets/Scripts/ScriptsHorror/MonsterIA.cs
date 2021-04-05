using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIA : MonoBehaviour
{
   [SerializeField] private Transform[] targets;
    private float[,] stages = {{ 10, 30 },{6,28 },{ 5, 20 },{ 3, 15 },{ 3, 10 },{ 2, 5 } };



    private void Start()
    {
        
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
    }

}
