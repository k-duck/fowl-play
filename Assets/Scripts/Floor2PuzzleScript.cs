using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.UI;

public class Floor2PuzzleScript : MonoBehaviour
{
    [Header("Puzzle 1")]
    bool AirlockActive = false;
    bool AirlockAlive = false;

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
    public List<TMP_Text> KnobAnswerUi;


    [Header("Slider")]

    [Range(0f, 1f)] public float correctSlider1;
    [Range(0f, 1f)] public float correctSlider2;
    [Range(0f, 1f)] public float correctSlider3;
    [Range(0f, 1f)] public float correctSlider4;

    [SerializeField] private GameObject sliderUi;
    [SerializeField] private GameObject sliderUi2;
    [SerializeField] private GameObject sliderUi3;
    [SerializeField] private GameObject sliderUi4;

    [Space(10, order = 2)]

    [SerializeField] private GameObject OverRide;


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


    [Space(20, order = 2)]

    public AudioSource LeverAudio;
    public AudioSource AirlockHighAudio;
    public AudioSource AirlockLowAudio;
    public AudioClip airlockStartNoise;

    public ElevatorDoors ElevatorDoor1;
    public ElevatorDoors ElevatorDoor2;

    [Space(20, order = 2)]

    public ElevatorDoors ArchiveDoor_01;
    public ElevatorDoors ArchiveDoor_02;
    public ElevatorDoors ArchiveDoor_03;
    public ElevatorDoors ArchiveDoor_04;
    public ElevatorDoors ArchiveDoor_05;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("puzzlescript started");
        AirlockOff();
        StartUpAirlock();
        GetReset();

        StartCoroutine(StartScene());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(3f);
        ElevatorDoor1.TriggerDoors();
    }
    //trigger door
    //ElevatorDoor1.TriggerDoors();


    // generator startup function;
    public void StartUpAirlock()
    {
        currentSlider1 = Slider1.GetComponent<XRSlider>().value;

        currentSlider2 = Slider2.GetComponent<XRSlider>().value;

        currentSlider3 = Slider3.GetComponent<XRSlider>().value;

        currentSlider4 = Slider4.GetComponent<XRSlider>().value;

        currentKnob1 = knob1.GetComponent<XRKnob>().value;

        currentKnob2 = knob2.GetComponent<XRKnob>().value;

    }

    // reset values of generator
    public void ResetAirlock()
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
        currentSlider1 = Slider1.GetComponent<XRSlider>().value;
        currentSlider2 = Slider2.GetComponent<XRSlider>().value;
        currentSlider3 = Slider3.GetComponent<XRSlider>().value;
        currentSlider4 = Slider4.GetComponent<XRSlider>().value;
        //Debug.Log(currentSlider1);
    }
    public void changeKnob()
    {
        currentKnob1 = knob1.GetComponent<XRKnob>().value;
        currentKnob2 = knob2.GetComponent<XRKnob>().value;
    }

    public void CheckAnswer()
    {
        LeverAudio.Play();

        GetSlider();

        //Debug.Log("Guess: " + currentKnob1);
        //Debug.Log("Guess: " + currentKnob2);
        //Debug.Log("Guess: " + currentSlider1);
        //Debug.Log("Guess: " + currentSlider2);
        //Debug.Log("Guess: " + currentSlider3);
        //Debug.Log("Guess: " + currentSlider4);

        //Debug.Log("Answer: " + correctKnob1);
        //Debug.Log("Answer: " + correctKnob2);
        //Debug.Log("Answer: " + correctSlider1);
        //Debug.Log("Answer: " + correctSlider2);
        //Debug.Log("Answer: " + correctSlider3);
        //Debug.Log("Answer: " + correctSlider4);

        if (currentKnob1 == correctKnob1 && currentKnob2 == correctKnob2)
        {
            //Debug.Log(currentKnob1);
            //Debug.Log(currentKnob2);
            if (currentSlider1 > (correctSlider1 - 0.20f) && currentSlider1 < (correctSlider1 + 0.20f))
            {
                // Debug.Log(currentSlider1);
                if (currentSlider2 > (correctSlider2 - 0.20f) && currentSlider2 < (correctSlider2 + 0.20f))
                {
                    // Debug.Log(currentSlider2);
                    if (currentSlider3 > (correctSlider3 - 0.20f) && currentSlider3 < (correctSlider3 + 0.20f))
                    {
                        //Debug.Log(currentSlider3);
                        if (currentSlider4 > (correctSlider4 - 0.20f) && currentSlider4 < (correctSlider4 + 0.20f))
                        {
                            //Debug.Log(currentSlider4);
                            Debug.Log("Airlock On");

                            AirlockOn();
                            AirlockActive = true;
                        }
                    }
                }
            }
        }


        if (AirlockActive == false)
        {
            Debug.Log("Airlock fail");
            //ResetAirlock();
            StartUpAirlock();
        }

    }

    public void AirlockOff()
    {
        // Randomize slider
        correctSlider1 = Random.Range(0f, 1f);
        correctSlider1 = Mathf.Round(correctSlider1 * 100) / 100;
        correctSlider2 = Random.Range(0f, 1f);
        correctSlider2 = Mathf.Round(correctSlider2 * 100) / 100;
        correctSlider3 = Random.Range(0f, 1f);
        correctSlider3 = Mathf.Round(correctSlider3 * 100) / 100;
        correctSlider4 = Random.Range(0f, 1f);
        correctSlider4 = Mathf.Round(correctSlider4 * 100) / 100;

        // change ui to show correct value
        sliderUi.GetComponent<Slider>().value = correctSlider1;
        sliderUi2.GetComponent<Slider>().value = correctSlider2;
        sliderUi3.GetComponent<Slider>().value = correctSlider3;
        sliderUi4.GetComponent<Slider>().value = correctSlider4;

        //Randomize ui value

        correctKnob1 = Random.Range(0, 2);
        if (correctKnob1 == 0)
        {
            KnobAnswerUi[0].SetText("Low");
        }
        else
        {
            KnobAnswerUi[0].SetText("High");
        }

        correctKnob2 = Random.Range(0, 2);
        if (correctKnob2 == 0)
        {
            KnobAnswerUi[1].SetText("Close");
        }
        else
        {
            KnobAnswerUi[1].SetText("Open");
        }

        Debug.Log("Airlock Values Set");
    }
    public void AirlockOn()
    {
        if (!AirlockAlive)
        {
            AirlockHighAudio.PlayOneShot(airlockStartNoise, 1);
            AirlockHighAudio.PlayDelayed(2.25f);
            AirlockLowAudio.Play();

            ArchiveDoor_01.TriggerDoors();
            ArchiveDoor_02.TriggerDoors();
            ArchiveDoor_03.TriggerDoors();
            ArchiveDoor_04.TriggerDoors();
            ArchiveDoor_05.TriggerDoors();

            AirlockAlive = true;
        }
        
    }

    public void SendManual()
    {
        OverRide.SetActive(true);
        Debug.Log("Airlock Manual Sent");
    }

    
}
