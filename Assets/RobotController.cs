using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private GameObject emptyLocation;
    [SerializeField]private GameObject PlacedItem;
    [SerializeField] public bool isRobotBird;
    [SerializeField] public bool isRobotHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ignore")
        {
            if (PlacedItem == null || PlacedItem == emptyLocation)
            {
                PlacedItem = other.gameObject;
                if (PlacedItem.tag == "RobotBird")
                {
                    isRobotBird = true;
                }
                else if (PlacedItem.tag == "RobotHand")
                {
                    isRobotHand = true;
                }

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other != PlacedItem)
        {
            PlacedItem = emptyLocation;
            isRobotBird = false;
            isRobotHand = false;
        }
    }

    public void Distraction()
    {

    }

    public void ButtonPress()
    {

    }


}
