using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public List<Camera> cam;
    public int CamNum;
    // Start is called before the first frame update
    void Start()
    {
        
        Display.displays[0].Activate();
        Display.displays[1].Activate();
        Display.displays[2].Activate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change()
    {
        Debug.Log("interact");
        if (CamNum == 0)
        {
            // wow
            cam[0].GetComponent<Camera>().targetDisplay = 0;
            //cam[0].GetComponent<Camera>()

            cam[1].GetComponent<Camera>().targetDisplay = 1;
            //Display.displays[0].Activate();
            //Display.displays[3].Activate();
            CamNum = 1;

        }
        else
        {
            cam[1].GetComponent<Camera>().targetDisplay = 0;
            cam[0].GetComponent<Camera>().targetDisplay = 1;
            //Display.displays[0].Activate();
            //Display.displays[3].Activate();
            CamNum = 0;
        }
    }
}
