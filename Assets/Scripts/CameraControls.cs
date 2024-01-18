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
        //Display.displays[1].Activate();
        //Display.displays[2].Activate();
        cam[0].GetComponent<Camera>().targetDisplay = 0;
        CamNum = 0;
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
            cam[2].GetComponent<Camera>().targetDisplay = 2;

            cam[3].GetComponent<Camera>().targetDisplay = 3;
            CamNum = 1;

        }else if (CamNum == 1)
        {
            cam[0].GetComponent<Camera>().targetDisplay = 1;

            cam[1].GetComponent<Camera>().targetDisplay = 2;

            cam[2].GetComponent<Camera>().targetDisplay = 3;

            cam[3].GetComponent<Camera>().targetDisplay = 0;
            CamNum = 2;
        }
        else if (CamNum == 2)
        {
            cam[0].GetComponent<Camera>().targetDisplay = 2;

            cam[1].GetComponent<Camera>().targetDisplay = 3;

            cam[2].GetComponent<Camera>().targetDisplay = 0;

            cam[3].GetComponent<Camera>().targetDisplay = 1;
            CamNum = 3;
        }
        else if (CamNum == 3)
        {
            cam[0].GetComponent<Camera>().targetDisplay = 3;

            cam[1].GetComponent<Camera>().targetDisplay = 0;

            cam[2].GetComponent<Camera>().targetDisplay = 1;

            cam[3].GetComponent<Camera>().targetDisplay = 2;
            CamNum = 0;
        }
        else
        {
            cam[0].GetComponent<Camera>().targetDisplay = 3;

            cam[1].GetComponent<Camera>().targetDisplay = 0;

            cam[2].GetComponent<Camera>().targetDisplay = 1;

            cam[3].GetComponent<Camera>().targetDisplay = 2;
            CamNum = 0;
        }
    }
}
