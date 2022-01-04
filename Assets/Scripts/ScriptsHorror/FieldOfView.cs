using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask monstruoMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
   
   
    public float GetPuntuacion()
    {
        visibleTargets.Clear();
        Collider[] monstruo = Physics.OverlapSphere(transform.position, viewRadius, monstruoMask);
        
        float mindist = viewRadius;
        for (int i=0; i < monstruo.Length; i++)
        {
            Transform target = monstruo[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Debug.Log(Vector3.Angle(transform.forward, dirToTarget));
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                   Debug.Log("Hola, he visto un lindo monstruito");
                    visibleTargets.Add(target);
                    if (disToTarget < mindist)
                    {
                        mindist = disToTarget;
                    }
                }
                else
                {
                    Debug.Log("Obstaculo");
                }
            }
        }
        return mindist / viewRadius;
    }
    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
}
