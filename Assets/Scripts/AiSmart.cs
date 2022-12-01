using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiSmart : MonoBehaviour
{
    private NavMeshAgent agent;

    public float radius;

    public Animator smartAnimator;
    private GameObject[] allSmart;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (allSmart == null)
        {
            allSmart = SmartManager.Instance.getListOfSmartGameObjects();
        }
        if (!agent.hasPath)
        {
            agent.SetDestination(GetPoints.Instance.GetRandomPoint(transform, radius));
            if (allSmart != null)
            {
                foreach (GameObject currentSmart in allSmart)
                {
                    currentSmart.GetComponent<Animator>();

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
