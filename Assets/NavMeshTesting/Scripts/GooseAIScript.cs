using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



abstract public class State
{
    abstract public void enterState(Goose goose);

    abstract public void updateState(Goose goose);
}

public class wanderState : State
{
    public override void enterState(Goose goose)
    {
        Debug.Log("Entered Wander State");
    }

    public override void updateState(Goose goose)
    {
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                //if (!gooseAgent.hasPath || gooseAgent.velocity.sqrMagnitude == 0f)
                //{
                GameObject newTarget = goose.currentTarget.GetComponent<TargetScript>().nextTargets[Random.Range(0, goose.currentTarget.GetComponent<TargetScript>().nextTargets.Length)];
                if (newTarget != goose.lastTarget || goose.currentTarget.GetComponent<TargetScript>().nextTargets.Length == 1)
                {
                    goose.lastTarget = goose.currentTarget;
                    goose.currentTarget = newTarget;
                }
                goose.gooseAgent.destination = goose.currentTarget.transform.position;
                //gooseAgent.destination = targets[Random.Range(0, targets.Length)].transform.position;
                //}
            }
        }
    }
}

public class Goose
{
    public State currentState;
    public NavMeshAgent gooseAgent;
    public Transform inititalGoal;
    public float targetBuffer;
    public GameObject[] targets;
    public GameObject currentTarget, lastTarget;
    public Goose(NavMeshAgent gAgent, GameObject[] t, GameObject cTarget, float tBuffer)
    {
        gooseAgent = gAgent;
        targets = t;
        currentTarget = cTarget;
        gooseAgent.destination = currentTarget.transform.position;

        currentState = new wanderState();
        currentState.enterState(this);
    }

    public void updateGoose()
    {
        currentState.updateState(this);
    }
}

public class GooseAIScript : MonoBehaviour
{
    public Goose gooseEnemy;
    public NavMeshAgent gooseAgent;
    public Transform inititalGoal;
    public float targetBuffer;
    private GameObject[] targets;
    private GameObject currentTarget, lastTarget;

    void Start()
    {
        
        //gooseAgent = GetComponent<NavMeshAgent>();
        //targets = GameObject.FindGameObjectsWithTag("GooseTarget");
        //currentTarget = inititalGoal.gameObject;
        //gooseAgent.destination = currentTarget.transform.position;
        gooseEnemy = new Goose(GetComponent<NavMeshAgent>(), GameObject.FindGameObjectsWithTag("GooseTarget"), inititalGoal.gameObject, targetBuffer);
    }

// Update is called once per frame
void Update()
    {
        gooseEnemy.updateGoose();
        /**
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
        **/

    }
}


