using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;
using TMPro;
using UnityEngine.UI;

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
    public List<TMP_Text> KnobAnswerUi;

    [Header("Slider")]
    

    [Range(0f,1f)] public float correctSlider1;
    [Range(0f, 1f)] public float correctSlider2;
    [Range(0f, 1f)] public float correctSlider3;
    [Range(0f, 1f)] public float correctSlider4;

    [SerializeField]private GameObject sliderUi;
    [SerializeField] private GameObject sliderUi2;
    [SerializeField] private GameObject sliderUi3;
    [SerializeField] private GameObject sliderUi4;


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


    [Header("Puzzle 3")]
    [Space(40, order = 2)]

    public GameObject Posters;
    List<int> PosterPool = new List<int> { 0, 1, 2, 3, 4 }; //int values are equal to grandchild num, so order isn't a problem
    int PosterCountMAX = 5;

    int guessNum = 0;
    public List<int> password;
    public List<int> passwordGuess;

    public List<TMP_Text> codeAnswers;

    public Animator ElevatorDoorR;
    public Animator ElevatorDoorL;

    [Space(40, order = 2)]
    [Header("Puzzle 3")]
    [Space(40, order = 2)]
    public bool Sceneflag1;
    public bool Sceneflag2;
    public bool Sceneflag3;

    [Header("Goose")]
    [Space(40, order = 2)]
    [SerializeField] GameObject GooseScript;



    // Start is called before the first frame update
    void Start()
    {
        GeneratorOff();
        StartUpGenerator();
        GetReset();

        RandomizePosters();
        
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
                if ( currentSlider1 > (correctSlider1 - 0.20f) && currentSlider1 < (correctSlider1 + 0.20f) )
                {
                    Debug.Log(currentSlider1);
                    if (currentSlider2 > (correctSlider2 - 0.20f) && currentSlider2 < (correctSlider2 + 0.20f))
                    {
                        Debug.Log(currentSlider2);
                        if (currentSlider3 > (correctSlider3 - 0.20f) && currentSlider3 < (correctSlider3 + 0.20f))
                        {
                            Debug.Log(currentSlider3);
                            if (currentSlider4 > (correctSlider4 - 0.20f) && currentSlider4 < (correctSlider4 + 0.20f))
                            {
                                Debug.Log(currentSlider4);
                                Debug.Log("generator On");

                                GeneratorOn();
                                GeneratorActive = true;
                                Sceneflag2 = true;
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
        if(correctKnob1 == 0)
        {
            KnobAnswerUi[0].SetText("3. Turn fuel valve to off");
        }
        else
        {
            KnobAnswerUi[0].SetText("3. Turn fuel valve to on");
        }

        correctKnob2 = Random.Range(0, 2);
        if (correctKnob2 == 0)
        {
            KnobAnswerUi[1].SetText("4. Turn dial to run");
        }
        else
        {
            KnobAnswerUi[1].SetText("4. Turn dial to choke");
        }



        // turn off all lights
        foreach (GameObject lights in Lights)
        {
            lights.gameObject.SetActive(false) ;
        }
    }
    public void GeneratorOn()
    {
        GooseScript.GetComponent<ReleseTheGoose>().ShowTheGoose();
        foreach (GameObject lights in Lights)
        {
            lights.gameObject.SetActive(true);
            
        }
    }

    ///Puzzle 3 assets
    ///
    
    public void RandomizePosters()
    {
        //pick random number within list length, activate that child of child 1, remove that number from poster pool, repeat for child 2 and so on
        for(int i = 0; i < PosterCountMAX; i++)
        {
            int rand = Random.Range(0, PosterPool.Count);
            Posters.transform.GetChild(i).GetChild(PosterPool[rand]).gameObject.SetActive(true);

            PosterPool.RemoveAt(rand);
        }
    }
    
    public void GetKeypadInput(int num)
    {
        if(Sceneflag2 == true) { 
            if (guessNum < 4)
            {
                passwordGuess[guessNum] = num;
                codeAnswers[guessNum].SetText(num.ToString());
                guessNum++;
           
            }
        }
    }
    public void CheckKeypadInput()
    {
        // this is a terrible way of doing this. todo fix this crap
        if(password[0] == passwordGuess[0] && password[1] == passwordGuess[1]&& password[2] == passwordGuess[2] && password[3] == passwordGuess[3] )
        {
            //door open
            Debug.Log("doorOpen");
            ElevatorDoorL.SetBool("OpenDoor", true);
            ElevatorDoorR.SetBool("OpenDoor", true);
        }
        else
        {
            //Reset
            guessNum = 0;
            passwordGuess[0] = 0;
            codeAnswers[0].SetText("0");
            passwordGuess[1] = 0;
            codeAnswers[1].SetText("0");
            passwordGuess[2] = 0;
            codeAnswers[2].SetText("0");
            passwordGuess[3] = 0;
            codeAnswers[3].SetText("0");
        }
    }
}
