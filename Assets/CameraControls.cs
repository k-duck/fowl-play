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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CamNum == 0)
        {
            cam[0].GetComponent<Camera>().targetDisplay = 1;
            //cam[0].GetComponent<Camera>()
            
            cam[1].GetComponent<Camera>().targetDisplay = 2;
            Display.displays[2].Activate();
        }
        else
        {
            cam[1].GetComponent<Camera>().targetDisplay = 1;
            cam[0].GetComponent<Camera>().targetDisplay = 2;
            Display.displays[2].Activate();
        }
    }
}
