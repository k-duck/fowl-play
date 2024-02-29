using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiPadInteraction : MonoBehaviour
{
    public Material correct;
    public Material wrong;
    public Material BaseMat;
    private Material[] objectMaterials;

    public GameObject uiStation;

    bool FinishedPuzzle = false;

    public Animator DoorRight;
    public Animator DoorLeft;

    public GameObject keycards;

    public AudioSource doorAudio;

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
        if (other.tag == "Card" && FinishedPuzzle == false)
        {
            StartCoroutine(CheckCard(other));
        }
        //other.gameObject
    }

    public void CorrectAnswer()
    {
        FinishedPuzzle = true;
        objectMaterials[1] = correct;
        uiStation.gameObject.GetComponent<Renderer>().materials = objectMaterials;
        DoorRight.SetBool("OpenDoor", true);
        DoorLeft.SetBool("OpenDoor", true);
        doorAudio.PlayDelayed(0.25f);
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
        //if(other.transform == keycards.transform.GetChild((int)(keycards.GetComponent<cardTexture>().KeyCard.x)))
        if (other.GetComponent<CardScript>().isCorrectCard == true)
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