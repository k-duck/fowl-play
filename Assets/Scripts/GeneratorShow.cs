using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorShow : MonoBehaviour
{

    public Floor1PuzzleScript Puzzle;
    public Material MatOn;
    public Material MatOff;

    public GameObject Sign1;
    public GameObject Sign2;
    private Material[] objectMaterials1;
    private Material[] objectMaterials2;

    // Start is called before the first frame update
    void Start()
    {
        objectMaterials1 = Sign1.GetComponent<Renderer>().materials;
        objectMaterials1[0] = MatOff;
        Sign1.gameObject.GetComponent<Renderer>().materials = objectMaterials1;

        objectMaterials2 = Sign2.GetComponent<Renderer>().materials;
        objectMaterials2[0] = MatOff;
        Sign2.gameObject.GetComponent<Renderer>().materials = objectMaterials2;

        checkGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkGenerator()
    {
        if (Puzzle.activeGenerator == 0)
        {
            
            objectMaterials1[0] = MatOn;
            Sign1.gameObject.GetComponent<Renderer>().materials = objectMaterials1;

           
            objectMaterials2[0] = MatOff;
            Sign2.gameObject.GetComponent<Renderer>().materials = objectMaterials2;

        }
        else if(Puzzle.activeGenerator == 1)
        {
            
            objectMaterials1[0] = MatOff;
            Sign1.gameObject.GetComponent<Renderer>().materials = objectMaterials1;

            
            objectMaterials2[0] = MatOn;
            Sign2.gameObject.GetComponent<Renderer>().materials = objectMaterials2;
        }
    }

}
