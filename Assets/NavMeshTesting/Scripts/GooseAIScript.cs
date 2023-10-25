using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GooseAIScript : MonoBehaviour
{
    NavMeshAgent gooseAgent;
    public Transform inititalGoal;
    public float targetBuffer;
    private GameObject[] targets;
    private GameObject currentTarget, lastTarget;

    void Start()
    {
        gooseAgent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("GooseTarget");
        currentTarget = inititalGoal.gameObject;
        gooseAgent.destination = currentTarget.transform.position;
    }

// Update is called once per frame
void Update()
    {
        // Check if we've reached the destination
        if (!gooseAgent.pathPending)
        {
            if (gooseAgent.remainingDistance <= gooseAgent.stoppingDistance + targetBuffer)
            {
                //if (!gooseAgent.hasPath || gooseAgent.velocity.sqrMagnitude == 0f)
                //{
                    GameObject newTarget = currentTarget.GetComponent<TargetScript>().nextTargets[Random.Range(0, currentTarget.GetComponent<TargetScript>().nextTargets.Length)];
                    if(newTarget != lastTarget || currentTarget.GetComponent<TargetScript>().nextTargets.Length == 1)
                    {
                        lastTarget = currentTarget;
                        currentTarget = newTarget;
                    }    
                    gooseAgent.destination = currentTarget.transform.position;
                    //gooseAgent.destination = targets[Random.Range(0, targets.Length)].transform.position;
                //}
            }
        }

    }
}


