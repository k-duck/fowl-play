using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

abstract public class State
{
    abstract public void EnterState(Goose goose);

    abstract public void UpdateState(Goose goose);
}

public class WanderState : State
{
    float startTime;
    static float duration = 0;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Wander State");
        startTime = Time.time;

        goose.currentTarget = goose.GetNearestTargetPoint(goose.targets ,goose.gooseAgent.gameObject);
        goose.gooseAgent.destination = goose.currentTarget.transform.position;
    }
    public override void UpdateState(Goose goose)
    {   
        //If goose has arrived at target
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                GameObject newTarget = goose.currentTarget.GetComponent<TargetScript>().nextTargets[Random.Range(0, goose.currentTarget.GetComponent<TargetScript>().nextTargets.Length)];
                if (newTarget != goose.lastTarget || goose.currentTarget.GetComponent<TargetScript>().nextTargets.Length == 1)
                {
                    goose.lastTarget = goose.currentTarget;
                    goose.currentTarget = newTarget;
                }
                goose.gooseAgent.destination = goose.currentTarget.transform.position;
            }
        }
        //If goose is close enough to player to just attack
        if(Vector3.Distance(goose.player.transform.position, goose.gooseAgent.transform.position) < goose.attackRange)
        {
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= duration)
        {
            goose.switchState(new GoToAmbushState());

            //Or AmubushState <-- to be implemented
        }
    }
}

public class StalkState : State
{
    float startTime;
    static float duration = 60;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Stalk State");
        startTime = Time.time;
    }
    public override void UpdateState(Goose goose)
    {
        goose.currentTarget = goose.GetNearestTargetPoint(goose.targets, goose.player);
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        //If goose has arrived at target
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                goose.switchState(new AttackState());
            }
        }
        //If goose is close enough to player to just attack
        if (Vector3.Distance(goose.player.transform.position, goose.gooseAgent.transform.position) < goose.attackRange)
        {
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= duration)
        {
            goose.switchState(new WanderState());
            //Give up and go back to wandering
        }
    }
}

public class GoToAmbushState : State
{
    float startTime;
    static float duration = 60;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Go To Ambush State");
        startTime = Time.time;
    }
    public override void UpdateState(Goose goose)
    {
        goose.currentTarget = goose.GetNearestTargetPoint(goose.vents, goose.gooseAgent.gameObject);
        if(goose.currentTarget == goose.GetNearestTargetPoint(goose.vents, goose.player))
        {
            goose.currentTarget = goose.GetFarthestTargetPoint(goose.vents, goose.player);
        }
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        //If goose has arrived at target
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                //Debug.Log(Quaternion.Angle(goose.gooseAgent.transform.rotation, goose.currentTarget.transform.rotation));

                if(Quaternion.Angle(goose.gooseAgent.transform.rotation, goose.currentTarget.transform.rotation) > 2f)
                {
                    goose.gooseAgent.transform.rotation = Quaternion.Lerp(goose.gooseAgent.transform.rotation, goose.currentTarget.transform.rotation, 2 * Time.deltaTime);
                } else
                {
                    //goose.gooseAgent.Warp(goose.vents[Random.Range(0, goose.vents.Length)].transform.position);
                    goose.gooseAgent.Warp(goose.GetNearestTargetPoint(goose.vents, goose.player).transform.position);
                    goose.switchState(new InAmbushState());
                }
            }
        }
        //If goose is close enough to player to just attack
        if (Vector3.Distance(goose.player.transform.position, goose.gooseAgent.transform.position) < goose.attackRange)
        {
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= duration)
        {
            goose.switchState(new WanderState());
            //Give up and go back to wandering
        }
    }
}

public class InAmbushState : State
{
    float startTime;
    static float duration = 60;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Wait For Ambush State");
        startTime = Time.time;
    }
    public override void UpdateState(Goose goose)
    {

        //If goose is close enough to player to just attack
        if (Vector3.Distance(goose.player.transform.position, goose.gooseAgent.transform.position) < goose.attackRange)
        {
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= duration)
        {
            goose.switchState(new WanderState());
            //Give up and go back to wandering
        }
    }
}

public class AttackState : State
{
    float startTime;
    static float duration = 10;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Attack State");
        startTime = Time.time;
    }
    public override void UpdateState(Goose goose)
    {
        goose.currentTarget = goose.player;
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        //If goose has arrived at target
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                //Implemnt KILL code here
            }
        }
        //If goose is close enough to stay in attacking state
        if (Vector3.Distance(goose.player.transform.position, goose.gooseAgent.transform.position) < goose.attackRange)
        {
            //Reset start time (only flees if not in range for a set peirod of time)
            startTime = Time.time;
        }
        //If enough time has passed far away from player
        if (Time.time - startTime >= duration)
        {
            goose.switchState(new FleeState());
        }
    }
}

public class FleeState : State
{
    float startTime;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Flee State");
        startTime = Time.time;
    }
    public override void UpdateState(Goose goose)
    {
        goose.currentTarget = goose.GetFarthestTargetPoint(goose.targets ,goose.player);
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        //If gooses is currently at the farthest point from the player  
        if (!goose.gooseAgent.pathPending)
        {
            if (goose.gooseAgent.remainingDistance <= goose.gooseAgent.stoppingDistance + goose.targetBuffer)
            {
                goose.switchState(new WanderState());
            }
        }
        //If enough time has passed to enter next state
        if (Time.time - startTime >= 60)
        {
            goose.switchState(new WanderState());
        }
    }
}

public class Goose
{
    State currentState;
    public NavMeshAgent gooseAgent;
    public Transform inititalGoal;
    public float targetBuffer;
    public GameObject[] targets;
    public GameObject[] vents;
    public GameObject currentTarget, lastTarget;
    public float attackRange;
    public GameObject player;
    public Goose(NavMeshAgent gAgent, GameObject[] t, float tBuffer, float dRange)
    {
        gooseAgent = gAgent;
        targets = t;
        targetBuffer = tBuffer;
        attackRange = dRange;

        player = GameObject.FindGameObjectWithTag("Player");
        vents = GameObject.FindGameObjectsWithTag("Vent");

        currentState = new WanderState();
        currentState.EnterState(this);
    }

    public void updateGoose()
    {
        currentState.UpdateState(this);
    }

    public void switchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public GameObject GetNearestTargetPoint(GameObject[] targetList, GameObject targetObject)        //Get nearest target point to a game object
    {
        GameObject closestTarget = null;
        float closestDistance = 100000000;
        foreach(GameObject target in targetList){
            float testDistance = Vector3.Distance(targetObject.transform.position, target.transform.position);
            if (testDistance < closestDistance)
            {
                closestDistance = testDistance;
                closestTarget = target;
            };
        }
        return closestTarget;
    }

    public GameObject GetFarthestTargetPoint(GameObject[] targetList, GameObject targetObject)   //Get farthest target point to a game object
    {
        GameObject farthestTarget = null;
        float farthestDistance = 0;
        foreach (GameObject target in targetList)
        {
            float testDistance = Vector3.Distance(targetObject.transform.position, target.transform.position);
            if (testDistance > farthestDistance)
            {
                farthestDistance = testDistance;
                farthestTarget = target;
            };
        }
        return farthestTarget;
    }
}

public class GooseAIScript : MonoBehaviour
{
    public Goose gooseEnemy;
    public NavMeshAgent gooseAgent;
    public float targetBuffer;
    private GameObject[] targets;
    private GameObject currentTarget, lastTarget;

    [SerializeField] private float attackRange;

    void Start()
    {
        gooseEnemy = new Goose(GetComponent<NavMeshAgent>(), GameObject.FindGameObjectsWithTag("GooseTarget"), targetBuffer, attackRange);
    }

// Update is called once per frame
void Update()
    {
        gooseEnemy.updateGoose();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


