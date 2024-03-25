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

    [SerializeField] private TextMeshProUGUI BirdText;
    [SerializeField] private TextMeshProUGUI RobotText;


    // Start is called before the first frame update
    void Start()
    {
        spawnRobot();
        SpawnBird();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void SpawnBird()
    {
        // get random number from number of spawns
        int random;
        random = Random.Range(0, BirdSpawns.Count);

        //spawn bird at random
        Bird.transform.position = BirdSpawns[random].transform.position;
        Debug.Log(random);
    }
    private void spawnRobot()
    {
        // get random number from number of spawns
        int random;
        random = Random.Range(0, ArmSpawns.Count);

        //spawn bird at random
        Arm.transform.position = ArmSpawns[random].transform.position;
        Debug.Log(random);
    }



}
