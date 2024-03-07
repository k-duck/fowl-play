using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteraction : MonoBehaviour
{
    public Material correct;
    public Material wrong;
    public Material BaseMat;
    private Material[] objectMaterials;

    public GameObject cardReader;

    bool FinishedCard = false;

    bool FinishedPuzzle = false;

    public Animator lid;

    public AudioSource doorAudio;

    public string chestContents;

    public SkullScript skulls;

    // Start is called before the first frame update
    void Start()
    {
        objectMaterials = cardReader.GetComponent<Renderer>().materials;
        objectMaterials[1] = BaseMat;
        cardReader.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Card" && FinishedCard == false)
        {
            StartCoroutine(CheckCard(other));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collisionYes:)");
        if(other.collider.tag == "Skull" && FinishedCard == true && FinishedPuzzle == false)
        {
            CheckSkull(other);
        }
    }

    public void CorrectAnswer()
    {
        FinishedCard = true;
        objectMaterials[1] = correct;
        cardReader.gameObject.GetComponent<Renderer>().materials = objectMaterials;
        lid.Play("Chest_Open");
    }
    public void wrongAnswer()
    {
        objectMaterials[1] = wrong;
        cardReader.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }
    public void BackToBase()
    {
        objectMaterials[1] = BaseMat;
        cardReader.gameObject.GetComponent<Renderer>().materials = objectMaterials;
    }
    IEnumerator CheckCard(Collider other)
    {

        if (skulls.correctCards[this.gameObject.transform.parent.parent.GetSiblingIndex()] == other.gameObject)
        {
            CorrectAnswer();
        }
        else
        {
            wrongAnswer();
        }

        yield return new WaitForSeconds(1.5f);
        if (FinishedCard == false)
        {
            BackToBase();
        }

    }
    public void CheckSkull(Collision other)
    {
        if (other.gameObject.name == chestContents)
        {
            lid.Play("Chest_Close");
            FinishedPuzzle = true;
            
            if(chestContents == "owl")
            {
                skulls.owl = true;
            }
            else if (chestContents == "parrot")
            {
                skulls.parrot = true;
            }
            else if (chestContents == "mallard")
            {
                skulls.mallard = true;
            }
            else if (chestContents == "spoonbill")
            {
                skulls.spoonbill = true;
            }
        }
    }
}