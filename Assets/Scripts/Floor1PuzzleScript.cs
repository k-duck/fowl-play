using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class Floor1PuzzleScript : MonoBehaviour
{
    [Header("Lights")]
    [Space(40, order = 2)]
    public List<GameObject> Lights;

    [Header("Puzzle 1")]
    [Space(40, order = 2)]
    public int correctNumber;

    [Header("Puzzle 2")]
    bool GeneratorActive = false;

    [Space(40, order = 2)]
    [Header("Knob")]
    private int activeGenerator = 1;


    [Header("Knob")]
    [Tooltip("Value from 0-8")]
    public float correctKnob1;
    public float correctKnob2;

    private float Resetknob1;
    private float Resetknob2;

    private float currentKnob1;
    private float currentKnob2;


    public GameObject knob1;
    public GameObject knob2;

    [Header("Slider")]
    

    [Range(0f,1f)] public float correctSlider1;
    [Range(0f, 1f)] public float correctSlider2;
    [Range(0f, 1f)] public float correctSlider3;
    [Range(0f, 1f)] public float correctSlider4;


    private float ResetSlider1;
    private float ResetSlider2;
    private float ResetSlider3;
    private float ResetSlider4;

    private float currentSlider1;
    private float currentSlider2;
    private float currentSlider3;
    private float currentSlider4;
    
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
        GeneratorOff();
        StartUpGenerator();
        GetReset();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // generator startup function;
    public void StartUpGenerator()
    {
        currentSlider1 = Slider1.GetComponent<XRSlider>().value;
        
        currentSlider2 = Slider2.GetComponent<XRSlider>().value;
        
        currentSlider3 = Slider3.GetComponent<XRSlider>().value;
        
        currentSlider4 = Slider4.GetComponent<XRSlider>().value;
       
        currentKnob1 = knob1.GetComponent<XRKnob>().value;
        
        currentKnob2 = knob2.GetComponent<XRKnob>().value;
        
    }

    // reset values of generator
    public void ResetGenerator()
    {
        Slider1.GetComponent<XRSlider>().value = ResetSlider1;
        Slider2.GetComponent<XRSlider>().value = ResetSlider2;
        Slider3.GetComponent<XRSlider>().value = ResetSlider3;
        Slider4.GetComponent<XRSlider>().value = ResetSlider4;
        knob1.GetComponent<XRKnob>().value = Resetknob1;
        knob2.GetComponent<XRKnob>().value = Resetknob2;

    }
    public void GetReset()
    {
        ResetSlider1 = currentSlider1;
        ResetSlider2 = currentSlider2;
        ResetSlider3 = currentSlider3;
        ResetSlider4 = currentSlider4;
        Resetknob1 = currentKnob1;
        currentKnob1 = Resetknob1;
    }

    public void GetSlider()
    {
       currentSlider1 =  Slider1.GetComponent<XRSlider>().value;
       currentSlider2 = Slider2.GetComponent<XRSlider>().value;
        currentSlider3 = Slider3.GetComponent<XRSlider>().value;
        currentSlider4 = Slider4.GetComponent<XRSlider>().value;
        Debug.Log(currentSlider1);
    }
    public void getLever(int num)
    {

    }
    public void changeKnob()
    {
        currentKnob1 = knob1.GetComponent<XRKnob>().value;
        currentKnob2 = knob2.GetComponent<XRKnob>().value;
    }
    public void SwitchGenerator(int num)
    {
        activeGenerator = num;
    }
    public void CheckAnswer()
    {
        Debug.Log(activeGenerator);
        Debug.Log(currentKnob1);
        Debug.Log(currentKnob2);
        Debug.Log(currentSlider1);
        Debug.Log(currentSlider2);
        Debug.Log(currentSlider3);
        Debug.Log(currentSlider4);

        if (activeGenerator == 0)
        {
            Debug.Log(activeGenerator);
            if (currentKnob1 == correctKnob1 && currentKnob2 == correctKnob2)
            {
                Debug.Log(currentKnob1);
                Debug.Log(currentKnob2);
                if ( currentSlider1 > (correctSlider1 - 0.10f) && currentSlider1 < (correctSlider1 + 0.10f) )
                {
                    Debug.Log(currentSlider1);
                    if (currentSlider2 > (correctSlider2 - 0.10f) && currentSlider2 < (correctSlider2 + 0.10f))
                    {
                        Debug.Log(currentSlider2);
                        if (currentSlider3 > (correctSlider3 - 0.10f) && currentSlider3 < (correctSlider3 + 0.10f))
                        {
                            Debug.Log(currentSlider3);
                            if (currentSlider4 > (correctSlider4 - 0.10f) && currentSlider4 < (correctSlider4 + 0.10f))
                            {
                                Debug.Log(currentSlider4);
                                Debug.Log("generator On");

                                GeneratorOn();
                                GeneratorActive = true;
                            }




                        }




                    }



                }

            }
        }

        Debug.Log("generator fail");
        if(GeneratorActive == false) { 
        ResetGenerator();
        StartUpGenerator();
        }

    }

    public void GeneratorOff()
    {
        foreach(GameObject lights in Lights)
        {
            lights.gameObject.SetActive(false) ;
        }
    }
    public void GeneratorOn()
    {
        foreach (GameObject lights in Lights)
        {
            lights.gameObject.SetActive(true);
        }
    }


}
