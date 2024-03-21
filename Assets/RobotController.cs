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
    public bool CanEffectCage;

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
        // checks if the object put in the lock is a new item and is not tagged as ignore
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

        // set everything to null if objet is removed;
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
        // bring cage up or down depending on the situation
        if (cageDown == false)
        {
            cageDown = true;
        }
        else
        {
            cageDown = false;
        }
        
       
        
        yield return new WaitForSeconds(1.3f);
        cage.SetBool("CageFall", cageDown);

    }
    public void Distraction()
    {
        PlacedItem.GetComponent<Animator>().SetTrigger("StateChange");
    }

    // function for buttons to activate hand
    public void ButtonPress()
    {
        // check if its robot hand.
        if(PlacedItem.tag == "RobotHand")
        {
            PlacedItem.GetComponent<Animator>().SetBool("Press", true);
            // check if location can effect cage;
            if (CanEffectCage == true)
            {
                
                StartCoroutine(buttonPower());
            }
            
        }

        
        /*
        if (cageDown == false)
        {
            cageDown = true;
        }
        else
        {
            cageDown = false;
        }
        cage.SetBool("CageFall", cageDown);
        */
    }
    public void ButtonPressMainButton()
    {
        
                StartCoroutine(buttonPower());
         


       
    }

}
