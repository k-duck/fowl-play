using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [SerializeField] private GameObject emptyLocation;
    [SerializeField]private GameObject PlacedItem;
    [SerializeField] public bool isRobotBird;
    [SerializeField] public bool isRobotHand;
    bool powered;

    public Animator cage;
    public bool cageDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)){
            StartCoroutine(buttonPower());
        }
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
                    if(powered == false)
                    {
                        powered = true;
                        PlacedItem.GetComponent<Animator>().SetBool("Flying", true);
                    }
                        
                    
                }
                else if (PlacedItem.tag == "RobotHand")
                {
                    isRobotHand = true;
                    //PlacedItem.GetComponent<Animator>().SetBool("Flying", true);
                }

            }
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(other != PlacedItem)
        {

            if (isRobotBird == true){
                if (powered == true)
                {
                    powered = false;
                    PlacedItem.GetComponent<Animator>().SetBool("Flying", false);

                }
            }
            PlacedItem = emptyLocation;
            isRobotBird = false;
            isRobotHand = false;
            

        }
    }

    IEnumerator buttonPower()
    {

        if (cageDown == false)
        {
            cageDown = true;
        }
        else
        {
            cageDown = false;
        }
        
        PlacedItem.GetComponent<Animator>().SetBool("Press",true);
        yield return new WaitForSeconds(1.3f);
        cage.SetBool("CageFall", cageDown);

    }
    public void Distraction()
    {
        PlacedItem.GetComponent<Animator>().SetTrigger("StateChange");
    }

    public void ButtonPress()
    {


        if (cageDown == false)
        {
            cageDown = true;
        }
        else
        {
            cageDown = false;
        }
        cage.SetBool("CageFall", cageDown);
    }


}
