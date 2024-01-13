using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class Floor1PuzzleScript : MonoBehaviour
{
    [Header("Puzzle 1")]

    public int correctNumber;

    [Header("Puzzle 2")]

    public float correctKnob1;
    
    public float correctSlider1;
    public float correctLever1;
    float currentKnob1;
    float currentSlider1;
    float currentLever1;
    public GameObject Slider1;

    // Start is called before the first frame update
    void Start()
    {
        GetSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSlider()
    {
       currentSlider1 =  Slider1.GetComponent<XRSlider>().value;
        Debug.Log(currentSlider1);
    }
    public void getLever(int num)
    {

    }

    public void checkAnswer()
    {

    }
}
