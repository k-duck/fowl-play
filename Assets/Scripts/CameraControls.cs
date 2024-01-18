using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{

    public List<Camera> cam;
    int numCameras;
    public int CamNum;

    public List<RawImage> mapRooms;
    // Start is called before the first frame update
    void Start()
    {
        numCameras = cam.Count;
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

    void UpdateMap()
    {
        foreach (RawImage room in mapRooms)
        {
            room.enabled = false;
        }
        mapRooms[(CamNum + 3) % numCameras].enabled = true;
    }

    public void change()
    {
        Debug.Log("interact");

        CamNum++;
        CamNum = (CamNum) % numCameras;

        cam[1].GetComponent<Camera>().targetDisplay = (CamNum) % numCameras;
        cam[2].GetComponent<Camera>().targetDisplay = (CamNum + 1) % numCameras;
        cam[3].GetComponent<Camera>().targetDisplay = (CamNum + 2) % numCameras;
        cam[0].GetComponent<Camera>().targetDisplay = (CamNum + 3) % numCameras;

        UpdateMap();

        /*
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
        */
    }

    public void GoToCam(int camID)
    {
        cam[1].GetComponent<Camera>().targetDisplay = (camID) % numCameras;
        cam[2].GetComponent<Camera>().targetDisplay = (camID + 1) % numCameras;
        cam[3].GetComponent<Camera>().targetDisplay = (camID + 2) % numCameras;
        cam[0].GetComponent<Camera>().targetDisplay = (camID + 3) % numCameras;
        CamNum = (camID) % numCameras;

        UpdateMap();
    }
}
