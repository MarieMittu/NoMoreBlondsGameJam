using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    private NavMeshAgent agent;

    public float radius;

    public Animator biondaAnimator;
    private GameObject[] allBionde;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
	if (allBionde == null)
        {
           allBionde = BiondeManager.Instance.getListOfBlondGameObjects();
        }
        if (!agent.hasPath)
        {
            agent.SetDestination(GetPoints.Instance.GetRandomPoint(transform, radius));
            if (allBionde != null)
            {
               foreach (GameObject currentBionda in allBionde) 
	       {
                    currentBionda.GetComponent<Animator>();
                    
               }
            }
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}
