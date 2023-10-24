using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    [ReadOnlyInspector] public bool canSeePlayer;
    [ReadOnlyInspector] public Transform TargetPlayer;
    [ReadOnlyInspector] public Enemy Enemy;

    public UnityAction EnemyDetected;
    public UnityAction EnemyUndetected;

    

    [ContextMenu("Getcomponents")]
    public void GetComponents()
    {
        Enemy = GetComponent<Enemy>();
    }

    

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius,targetMask);
        
        if (rangeChecks.Length > 0)
        {
            
            SortArrayAcordingToDistance(ref rangeChecks);
            
            //For loop ile en yakýndakine döndürebilirsin birden fazla obje için;
            Transform target = rangeChecks[0].transform;

            for (int i = 0; i < rangeChecks.Length; i++)
            {
                
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position , target.position);

                    if (!Physics.Raycast(transform.position , directionToTarget, distanceToTarget, obstructionMask))
                    {
                        if (!canSeePlayer)
                        {
                            
                            EnemyDetected.Invoke();
                        }
                        Enemy.TargetPlayer = target;
                        TargetPlayer = target;
                        canSeePlayer = true;
                        break;
                    }
                    else
                    {
                        if (TargetPlayer != null)
                        {
                            EnemyUndetected.Invoke();
                        }
                        Enemy.TargetPlayer = null;
                        TargetPlayer = null;
                        canSeePlayer = false;
                    }

                }
                else
                {
                    
                    if (TargetPlayer != null)
                    {
                        EnemyUndetected.Invoke();
                    }
                    target = SelectNewTarget(ref rangeChecks, i);
                    TargetPlayer = null;
                    Enemy.TargetPlayer = null;
                    canSeePlayer = false;

                    
                }
            }

        }
        else if (canSeePlayer)
        {
            if (TargetPlayer != null)
            {
                EnemyUndetected.Invoke();
            }
            TargetPlayer = null;
            canSeePlayer = false;
        }

    }

    

    private Transform SelectNewTarget(ref Collider[] rangeChecks, int i)
    {
        Transform result=null;
        if(rangeChecks.Length-1>i)
            result= rangeChecks[i + 1].transform;
        return result;
    }

    public void SortArrayAcordingToDistance(ref Collider[] pCollArr)
    {
        bool swapped;
        for(int i = 0; i < pCollArr.Length-1; i++)
        {
            swapped = false;
            for (int j = 0; j < pCollArr.Length - 1-i; j++)
            {
                if (!IsLastIndexFurthest(pCollArr[j], pCollArr[j+1]))
                {
                    SwapColliders(ref pCollArr[j], ref pCollArr[j+1]);
                    swapped= true;
                }
            }

            if (!swapped)
                break;
        }

    }

    public bool IsLastIndexFurthest(Collider A, Collider B)
    {
        float distanceToA = Vector3.Distance(A.transform.position, transform.position);
        float distanceToB = Vector3.Distance(B.transform.position, transform.position);
        
        return distanceToA < distanceToB;   
    }
    public void SwapColliders(ref Collider A, ref Collider B)
    {
        Collider temp = A;
        A = B;
        B = temp;
    }

    
}
