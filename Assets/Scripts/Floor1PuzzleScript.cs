using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class Floor1PuzzleScript : MonoBehaviour
{
    [Header("Puzzle 1")]
    [Space(40, order = 2)]
    public int correctNumber;

    [Header("Puzzle 2")]
    [Space(40, order = 2)]
   

    [Header("Knob")]
    [Tooltip("Value from 0-8")]
    public float correctKnob1;
    public float correctKnob2;
    float currentKnob1;
    float currentKnob2;

    public GameObject knob1;
    public GameObject knob2;

    [Header("Slider")]
    

    [Range(0f,1f)] public float correctSlider1;
    [Range(0f, 1f)] public float correctSlider2;
    [Range(0f, 1f)] public float correctSlider3;
    [Range(0f, 1f)] public float correctSlider4;
    

    float currentSlider1;
    float currentSlider2;
    float currentSlider3;
    float currentSlider4;
    
    public GameObject Slider1;
    public GameObject Slider2;
    public GameObject Slider3;
    public GameObject Slider4;

    [Header("Lever")]
    public float correctLever1;
    float currentLever1;
    //[Space(40, order = 2)]

    // Start is called before the first frame update
    void Start()
    {
        
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
