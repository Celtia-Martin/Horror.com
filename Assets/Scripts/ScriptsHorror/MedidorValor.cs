using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedidorValor : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask monstruoMask;
    public LayerMask obstacleMask;
    private Horror_com horrorRef;
    public void setHorrorCom(Horror_com horror)
    {
        horrorRef = horror;
        StartCoroutine(actualizarValor());
    }
    IEnumerator actualizarValor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (FindVisibleTargets())
            {
                
                horrorRef.Valor(-1.6f);
            }
            else
            {
                horrorRef.Valor(0.6f);
            }
           
        }
    }
    public bool FindVisibleTargets()
    {
        
        Collider[] monstruo = Physics.OverlapSphere(transform.position, viewRadius, monstruoMask);

        for (int i = 0; i < monstruo.Length; i++)
        {
            Transform target = monstruo[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    return true;

                }
            }

        }
        return false;

    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
}
