using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    public Text roomName;
    public List<string> roomNames;
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
        CamNum = 1;

        UpdateMap();
    }


    void UpdateMap()
    {
        foreach (RawImage room in mapRooms)
        {
            room.enabled = false;
        }
        mapRooms[(CamNum + 3) % numCameras].enabled = true;

        roomName.text = roomNames[(CamNum + 3) % numCameras];
    }

    public void change()
    {
        Debug.Log("interact");

        CamNum++;
        CamNum = (CamNum) % numCameras;

        for (int i = 0; i < numCameras; i++)
        {
            cam[(i + 1) % numCameras].GetComponent<Camera>().targetDisplay = (CamNum + i) % numCameras;
        }

        UpdateMap();
    }

    public void GoToCam(int camID)
    {
        for(int i=0; i<numCameras; i++)
        {
            cam[(i + 1) % numCameras].GetComponent<Camera>().targetDisplay = (camID + i) % numCameras;
        }

        CamNum = (camID) % numCameras;

        UpdateMap();
    }
}
