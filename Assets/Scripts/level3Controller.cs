using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class level3Controller : MonoBehaviour
{
    [SerializeField]private List<GameObject> BirdSpawns;
    
    [SerializeField]private List<GameObject> ArmSpawns;

    [SerializeField] private GameObject Bird;
    [SerializeField] private GameObject Arm;

    
    [SerializeField] private TMP_Text RobotText;

    
    public GameObject Trap;
    public GameObject doors;
    public int random;

    [SerializeField] private TMP_Text DoorLock;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnBird();
        spawnRobot();
    }

    // Update is called once per frame
    void Update()
    {
        if(Trap.GetComponent<TrapingGoose>().isGrooseTraped == true)
        {
            DoorLock.text = "Door Unlocked";
            DoorLock.color = Color.green;

        }
        else
        {
            DoorLock.text = "Error: Subject loose, door locked";
            DoorLock.color = Color.red;
        }
    }


    public void DoorOpen()
    {
        if(Trap.GetComponent<TrapingGoose>().isGrooseTraped == true)
        {
            //Door opens
            doors.GetComponent<ElevatorDoors>().TriggerDoors();

        }else
        {
            // no
        }
    }


    private void SpawnBird()
    {
        // get random number from number of spawns
        
        random = Random.Range(0, BirdSpawns.Count);

        //spawn bird at random
        Bird.transform.position = BirdSpawns[random].transform.position;
        Debug.Log(random);
    }
    private void spawnRobot()
    {
        // get random number from number of spawns
        
        random = Random.Range(0, ArmSpawns.Count);

        //spawn bird at random
        Arm.transform.position = ArmSpawns[random].transform.position;
        Debug.Log(random);

        switch(random)
        {
            case 0:
                break;
                RobotText.text="3-5: Robotics"; 
                break;
            case 1:
                RobotText.text = "3-5: nothing";
                break;
            case 2:
                RobotText.text = "3-5: Aviation";
                break;
        }

    }



}
