using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPadInteraction : MonoBehaviour
{
    public Material correct;
    public Material wrong;
    public Material BaseMat;
    private Material[] objectMaterials;

    public GameObject uiStation;

    bool FinishedPuzzle = false;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(uiStation.GetComponent<Renderer>().materials[1]);
        objectMaterials = uiStation.GetComponent<Renderer>().materials;
        objectMaterials[1] = BaseMat;
        uiStation.gameObject.GetComponent<Renderer>().materials = objectMaterials;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Card" && FinishedPuzzle == false)
        {
            Debug.Log("the fuck i am");
            StartCoroutine(CheckCard(other));
        }
        //other.gameObject
    }

    public void CorrectAnswer()
    {
        FinishedPuzzle = true;
        objectMaterials[1] = correct;
        uiStation.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }
    public void wrongAnswer()
    {
        objectMaterials[1] = wrong;
        uiStation.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }
    public void BackToBase()
    {
        objectMaterials[1] = BaseMat;
        uiStation.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }


    IEnumerator CheckCard(Collider other)
    {
        if(other.gameObject.GetComponent<CardScript>().isCorrectCard == true)
        {
            CorrectAnswer();
        }
        else
        {
            wrongAnswer();
        }

        yield return new WaitForSeconds(1.5f);
        if (FinishedPuzzle == false)
        {
            BackToBase();
        }

    }


}
