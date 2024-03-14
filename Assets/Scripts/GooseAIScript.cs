using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

abstract public class State
{
    abstract public void EnterState(Goose goose);

    abstract public void UpdateState(Goose goose);
}

public class WanderState : State
{
    float startTime;
    float offsetX, offsetY;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Wander State");
        startTime = Time.time;

        goose.currentTarget = goose.GetNearestTargetPoint(goose.targets ,goose.gooseAgent.gameObject);
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        offsetX = offsetY = 0;
        goose.gooseAgent.GetComponent<Collider>().enabled = true;
        goose.gooseAgent.speed = 2;
        if (goose.firstLoad == true)
        {
            goose.gooseAnimator.SetInteger("WalkMode", 0);
        } else
            goose.firstLoad = true;
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
                    offsetX = Random.Range(-2.5f, 2.5f);
                    offsetY = Random.Range(-2.5f, 2.5f);
                    goose.lastTarget = goose.currentTarget;
                    goose.currentTarget = newTarget;
                }
                goose.gooseAgent.destination = goose.currentTarget.transform.position + new Vector3(offsetX,offsetY);
            }
        }
        //If goose is in line of sight
        if(goose.IsLineOfSight(goose.EyePostion, goose.player))
        {
            goose.switchState(new AssessState());
            //goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= goose.wanderDuration)
        {
            if (Random.Range(1, 5) == 1)
            {
                float playerDistanceFromVent = Vector3.Distance(goose.GetNearestTargetPoint(goose.vents, goose.player).transform.position, goose.player.transform.position);
                if (playerDistanceFromVent < 15)
                {
                    goose.switchState(new GoToAmbushState());
                }
                else
                {
                    goose.switchState(new StalkState());
                }
            } else
            {
                startTime = startTime + 10;
                Debug.Log("Keep Wandering");
            }
        }
    }
}

public class AssessState : State
{
    Quaternion faceRotation;
    float startTime, lostTime;
    int stalkSubstate;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Assess State");
        startTime = lostTime = Time.time;

        goose.currentTarget = goose.player;
        goose.lastKnownLocation = goose.currentTarget.transform.position;
        goose.gooseAgent.speed = 2.5f;
        goose.gooseAnimator.SetInteger("WalkMode", 1);
        stalkSubstate = 0;
    }
    public override void UpdateState(Goose goose)
    {
        
        //If in line of sight, stare at the player
        if (goose.IsLineOfSight(goose.EyePostion, goose.player))      
        {          
            
            //If goose is close enough to just attack
            if (goose.distanceFromPlayer < goose.attackRange)
            {
                goose.gooseAgent.speed = 4;
                goose.switchState(new AttackState());
            }

            switch (stalkSubstate)
            {
                //Normal
                case 0:

                    goose.gooseAgent.speed = 1.5f;

                    if (Time.time - lostTime >= 0.2f)
                    {
                        goose.gooseAgent.destination = goose.gooseAgent.gameObject.transform.position;
                    }

                    //Stare at player
                    Vector3 lookPos = goose.player.transform.position - goose.gooseAgent.transform.position;
                    lookPos.y = 0;
                    faceRotation = Quaternion.LookRotation(lookPos);
                    goose.gooseAgent.transform.rotation = Quaternion.Lerp(goose.gooseAgent.transform.rotation, faceRotation, 2 * Time.deltaTime);

                    //Increase aggression, increases faster the closer the goose is to the player
                    goose.aggression += (0.2f / goose.distanceFromPlayer);
                    int attackChance = (int)(Mathf.InverseLerp(0, 150, goose.aggression) * 100);
                    

                    //Every set interval, chance to go to attack mode
                    if (Time.time - startTime >= goose.assessInterval)
                    {
                        Debug.Log("Goose Aggression at: " + goose.aggression + "\nGoose Attack Chance at: " + attackChance + "/200");
                        float stateChoice = Random.Range(0, 200);
                        if  (stateChoice <= attackChance)
                        {
                            goose.switchState(new AttackState());
                        } else if(stateChoice >= 100)
                        {
                            Debug.Log("Creep Closer");
                            goose.playHonkAudio();
                            stalkSubstate = 1;
                            startTime = Time.time;
                        }
                        else
                        {
                            startTime = startTime + Random.Range(4, 6);
                            Debug.Log("Do Nothing");
                        }
                    }
                    goose.lastKnownLocation = goose.currentTarget.transform.position;
                    break;
                //Short Approach
                case 1:
                    goose.gooseAgent.destination = goose.currentTarget.transform.position;
                    goose.gooseAgent.speed = 1f;

                    //Increase aggression, increases faster the closer the goose is to the player
                    goose.aggression += (0.1f / goose.distanceFromPlayer);

                    if (Time.time - startTime >= 10)
                    {
                        stalkSubstate = 0;
                    }
                    break;
            }

        }
        else
        {
            goose.gooseAgent.speed = 2.5f;
            //Player not vissible, go to last known location untill player is in line of sight again
            goose.gooseAgent.destination = goose.lastKnownLocation;
            lostTime = Time.time;

            if(goose.aggression > 0) goose.aggression -= 0.01f;
            int attackChance = (int)(Mathf.InverseLerp(0, 100, goose.aggression) * 100);
            //Debug.Log("Goose Aggression at: " + goose.aggression + "\nGoose Attack Chance at: " + attackChance + "/200");

            //If out of line of sight long enough, return to wandering
            if (Time.time - startTime >= goose.assessInterval * 2)
            {
                goose.switchState(new WanderState());
            }
        }
    }
}

public class StalkState : State
{
    float startTime;
    Quaternion faceRotation;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Stalk State");
        startTime = Time.time;
        goose.gooseAgent.speed = 2.5f;
        goose.gooseAnimator.SetInteger("WalkMode", 1);
    }
    public override void UpdateState(Goose goose)
    {
        //If goose is close enough to player to just attack
        if (goose.distanceFromPlayer < goose.attackRange && goose.IsLineOfSight360(goose.EyePostion, goose.player))
        {
            goose.switchState(new AttackState());
        }
        else if(goose.IsLineOfSight(goose.EyePostion, goose.player))
        {
            if (goose.distanceFromPlayer < 10)
            {
                goose.currentTarget = goose.GetFarthestTargetPoint(goose.targets, goose.player);
                goose.gooseAgent.destination = goose.currentTarget.transform.position;
            } else
            {
                goose.gooseAgent.destination = goose.gooseAgent.gameObject.transform.position;
                //Stare at player
                Vector3 lookPos = goose.player.transform.position - goose.gooseAgent.transform.position;
                lookPos.y = 0;
                faceRotation = Quaternion.LookRotation(lookPos);
                goose.gooseAgent.transform.rotation = Quaternion.Lerp(goose.gooseAgent.transform.rotation, faceRotation, 2 * Time.deltaTime);
            }
        } else
        {
            goose.currentTarget = goose.GetNearestTargetPoint(goose.targets, goose.player);
            goose.gooseAgent.destination = goose.currentTarget.transform.position;
        }
        
        
        //If enough time has passed to change state
        if (Time.time - startTime >= goose.stalkDuration)
        {
            goose.switchState(new FleeState());
            //Give up and go back to wandering
        }
    }
}

public class GoToAmbushState : State 
{
    float startTime, ventTime;
    static float duration = 60;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Go To Ambush State");
        startTime = Time.time;
        goose.gooseAgent.speed = 2;
        if (goose.firstLoad == true)
        {
            goose.gooseAnimator.SetInteger("WalkMode", 0);
        } else
            goose.firstLoad = true;
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

                    if (goose.gooseAnimator.GetBool("EnteringVent") == false)
                    {
                        goose.currentTarget.GetComponent<VentScript>().OpenVent();
                        goose.gooseAnimator.SetBool("InVent", false);
                        Debug.Log("Entering Vent");
                        goose.gooseAnimator.SetBool("EnteringVent", true);

                        ventTime = Time.time;
                    }

                    if(Time.time - ventTime >= goose.gooseAnimator.GetCurrentAnimatorStateInfo(0).length)
                    {
                        //goose.gooseAgent.Warp(goose.vents[Random.Range(0, goose.vents.Length)].transform.position);
                        GameObject nextVent = goose.GetNearestTargetPoint(goose.vents, goose.player);
                        goose.gooseAgent.Warp(nextVent.transform.position);
                        nextVent.GetComponent<VentScript>().GooseInVent();
                        goose.gooseAnimator.SetBool("EnteringVent", false);
                        goose.gooseAnimator.SetBool("InVent", true);
                        goose.switchState(new InAmbushState());
                    }
                }
            }
        }
        //If goose is close enough to player to just attack
        if (goose.distanceFromPlayer < goose.attackRange && goose.IsLineOfSight(goose.EyePostion, goose.player))
        {
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= duration || goose.gooseAgent.path.status != NavMeshPathStatus.PathComplete)
        {
            goose.switchState(new WanderState());
            //Give up and go back to wandering
        }
    }
}

public class InAmbushState : State
{
    float startTime;
    GameObject currentVent;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Wait For Ambush State");
        startTime = Time.time;

        Vector3 newRot = goose.GetNearestTargetPoint(goose.vents, goose.gooseAgent.gameObject).transform.rotation.eulerAngles;
        newRot = new Vector3(newRot.x, newRot.y+180, newRot.z);
        goose.gooseAgent.transform.rotation = Quaternion.Euler(newRot);

        goose.gooseAgent.GetComponent<Collider>().enabled = false;
        currentVent = goose.GetNearestTargetPoint(goose.vents, goose.gooseAgent.gameObject);
    }
    public override void UpdateState(Goose goose)
    {

        //If goose is close enough to player to just attack
        if (goose.distanceFromPlayer < goose.attackRange)
        {
            currentVent.GetComponent<VentScript>().GooseLeaveVent();
            goose.gooseAnimator.SetBool("InVent", false);
            goose.switchState(new AttackState());
        }
        //If enough time has passed to change state
        if (Time.time - startTime >= goose.ambushDuration)
        {
            currentVent.GetComponent<VentScript>().GooseLeaveVent();
            goose.gooseAnimator.SetBool("InVent", false);
            goose.switchState(new WanderState());
            //Give up and go back to wandering
        }
    }
}

public class AttackState : State
{
    float startTime, startTimeVent;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Attack State");
        startTime = startTimeVent = Time.time;
        goose.gooseAgent.speed = 4;
        goose.gooseAnimator.SetInteger("WalkMode", 1);
    }
    public override void UpdateState(Goose goose)
    {
        goose.currentTarget = goose.player;
        goose.gooseAgent.destination = goose.currentTarget.transform.position;

        //If goose is close enough to stay in attacking state
        if (goose.distanceFromPlayer < goose.attackRange
            && goose.IsLineOfSight(goose.EyePostion, goose.player))
        {
            
            //Reset start time (only flees if not in range for a set peirod of time)
            startTime = Time.time;
        }

        if (Time.time - startTimeVent >= 1.5f)
        {
            goose.gooseAgent.GetComponent<Collider>().enabled = true;
        }
        //If enough time has passed far away from player
        if (Time.time - startTime >= goose.attackDuration)
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
        goose.gooseAgent.speed = 3;
        goose.gooseAnimator.SetInteger("WalkMode", 0);
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
        if (Time.time - startTime >= goose.fleeDuration)
        {
            goose.switchState(new WanderState());
        }
    }
}

public class IdleState : State
{
    float startTime;
    public override void EnterState(Goose goose)
    {
        Debug.Log("Entered Idle State");
        startTime = Time.time;
        goose.gooseAgent.speed = 2;
        goose.gooseAnimator.SetInteger("WalkMode", 0);
    }
    public override void UpdateState(Goose goose)
    {
        goose.gooseAgent.destination = goose.gooseAgent.transform.position;
    }
}

public class Goose
{
    State currentState;
    public NavMeshAgent gooseAgent;
    public Transform inititalGoal;
    public GameObject[] targets, vents;
    public GameObject currentTarget, lastTarget, player;
    public AudioSource gooseAudio;
    public Vector3 lastKnownLocation;
    [SerializeField] public GameObject EyePostion;

    public float targetBuffer;
    public float attackRange;
    public float wanderDuration;
    public float stalkDuration;
    public float ambushDuration;
    public float attackDuration;
    public float fleeDuration;
    public float assessInterval;
    public float aggression;
    public float distanceFromPlayer;
    public AudioClip[] slapSFX;
    public AudioClip[] honkSFX;

    public Animator gooseAnimator;

    private float footstepStartTime;
    public bool firstLoad;
    UnityEvent gameOverEvent = new UnityEvent();
    public Goose(NavMeshAgent gAgent, float tBuffer, float dRange, float wDur, float sDur, float amDur, float atDur, float fDur, float aInterval, AudioClip[] slapClips, AudioClip[] honkClips, GameObject eyePos, Animator gAnimator)
    {
        gooseAgent = gAgent;
        targetBuffer = tBuffer;
        attackRange = dRange;
        wanderDuration = wDur;
        stalkDuration = sDur;
        ambushDuration = amDur;
        attackDuration = atDur;
        fleeDuration = fDur;
        assessInterval = aInterval;
        EyePostion = eyePos;

        aggression = 0;

        targets = GameObject.FindGameObjectsWithTag("GooseTarget");
        player = GameObject.FindGameObjectWithTag("Player");
        vents = GameObject.FindGameObjectsWithTag("Vent");
        gooseAudio = gAgent.GetComponent<AudioSource>();
        lastKnownLocation = gAgent.transform.position;

        //currentState = new GoToAmbushState();
        currentState = new WanderState();
        currentState.EnterState(this);
        slapSFX = slapClips;
        honkSFX = honkClips;
        footstepStartTime = Time.time;
        firstLoad = true;

        gooseAnimator = gAnimator;
    }

    public void updateGoose()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, gooseAgent.transform.position);
        currentState.UpdateState(this); 
    }

    public void playHonkAudio()
    {
        gooseAudio.pitch = Random.Range(0.8f, 1.2f);
        gooseAudio.PlayOneShot(honkSFX[Random.Range(0, honkSFX.Length)]);
        gooseAudio.pitch = 1;
    }

    public void switchState(State state)
    {
        playHonkAudio();
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

    public bool IsLineOfSight(GameObject one, GameObject two)
    {
        // one = goose, two= player
        var ray = new Ray(one.transform.position , two.transform.position - one.transform.position);
        var hits = Physics.RaycastAll(ray, ray.direction.magnitude * 100);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {   if(hit.collider.CompareTag("VisibleObject") || hit.collider.CompareTag("Player"))
                {
                    if (hit.collider.gameObject == one || hit.collider.gameObject == two) continue;
                    Debug.DrawLine(one.transform.position, hit.collider.transform.position, Color.red, 1);
                    Debug.DrawLine(one.transform.position, two.transform.position, Color.blue, 1);
                    
                    //Debug.Log($"Interferred by {hit.collider.name}");
                    if (Vector3.Distance(one.transform.position, hit.transform.position) < Vector3.Distance(one.transform.position, two.transform.position))
                    {
                        return false;
                    }
                }
            }
        }
        //Debug.Log(Vector3.Angle(one.transform.forward, two.transform.position - one.transform.position));
        if(Vector3.Angle(one.transform.forward, two.transform.position - one.transform.position) >  (90)) 
        {
            return false;
        }
        //Debug.Log("IN LINE OF SIGHT");
        Debug.DrawLine(one.transform.position, two.transform.position, Color.green, 1);
        return true;
    }
    public bool IsLineOfSight360(GameObject one, GameObject two)
    {
        var ray = new Ray(one.transform.position, two.transform.position - one.transform.position);
        var hits = Physics.RaycastAll(ray, ray.direction.magnitude * 100);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("VisibleObject") || hit.collider.CompareTag("Player"))
                {
                    if (hit.collider.gameObject == one || hit.collider.gameObject == two) continue;
                    Debug.DrawLine(one.transform.position, hit.collider.transform.position, Color.red, 1);
                    Debug.DrawLine(one.transform.position, two.transform.position, Color.blue, 1);

                    //Debug.Log($"Interferred by {hit.collider.name}");
                    if (Vector3.Distance(one.transform.position, hit.transform.position) < Vector3.Distance(one.transform.position, two.transform.position))
                    {
                        return false;
                    }
                }
            }
        }
        //Debug.Log("IN LINE OF SIGHT");
        return true;
    }

    public void attackPlayer()
    {
        //Play attack animation
        Debug.Log("Player Attacked!");
        playHonkAudio();

        GameOverScript.gameOverEvent.Invoke();

        switchState(new FleeState());
    }
}

public class GooseAIScript : MonoBehaviour
{
    public Goose gooseEnemy;
    public AudioClip[] slapSFX;
    public AudioClip[] honkSFX;

    [SerializeField] private float targetBuffer;
    [SerializeField] private float attackRange;
    [SerializeField] private float wanderDuration;
    [SerializeField] private float stalkDuration;
    [SerializeField] private float ambushDuration;
    [SerializeField] private float attackDuration;
    [SerializeField] private float fleeDuration;
    [SerializeField] private float assessInterval;
    [SerializeField] private Animator gooseAnimator;
    [SerializeField] private GameObject GooseEyes;


    void Start()
    {
        gooseEnemy = new Goose(GetComponent<NavMeshAgent>(), targetBuffer, attackRange
                                , wanderDuration, stalkDuration, ambushDuration, attackDuration, fleeDuration, assessInterval
                                , slapSFX, honkSFX, GooseEyes, gooseAnimator);
        //gooseEnemy.GetEyes(GooseEyes);
        
    }

    // Update is called once per frame
    void Update()
    {
        gooseEnemy.updateGoose();
        gooseAnimator.SetFloat("Speed", gooseEnemy.gooseAgent.velocity.magnitude);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("objecthit: " + trigger);

        if (trigger.tag == "Player" && gooseAnimator.GetBool("Attacking") != true && gooseAnimator.GetBool("InVent") != true)
        {
            gooseAnimator.SetBool("Attacking", true);
            StartCoroutine(PlayAttackAnimation());
        }
    }

    IEnumerator PlayAttackAnimation()
    {
        gooseEnemy.switchState(new IdleState());
        yield return new WaitForSeconds(2f);
        gooseAnimator.SetBool("Attacking", false);
        gooseEnemy.attackPlayer();

        yield return null;
    }

}


