using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameController : MonoBehaviour
{
    private UnityEngine.XR.InputDevice deviceRight;
    private UnityEngine.XR.InputDevice deviceLeft;

    private ActionBasedControllerManager controllerLeft;
    private ActionBasedControllerManager controllerRight;
    private GameObject tunnelControl;
    private GameObject rig;

    static bool moveType = false; // 0 = Smooth   1 = Teleport
    static bool turnType = false; // 0 = Smooth   1 = Snap
    static float turnStrength = 0.5f; // Turn value (degrees for snap turn, speed for smooth turn)
    static bool handedness = false; // 0 = Right   1 = Left
    static bool tunneling = true; // 0 = Off   1 = On
    static float tunnelStrength = 0.75f;



    // Start is called before the first frame update
    void Start()
    {
        rig = GameObject.Find("XR Origin (XR Rig)");

        tunnelControl = GameObject.Find("TunnelingVignette");

        Debug.Log("Rig: " + rig);
        Debug.Log("Tunnel: " + tunnelControl);
        //Debug.Log("Left Game Object: " + GameObject.FindGameObjectWithTag("Left"));

        controllerLeft = rig.transform.GetChild(0).GetChild(3).GetComponent<ActionBasedControllerManager>();

        controllerRight = rig.transform.GetChild(0).GetChild(5).GetComponent<ActionBasedControllerManager>();
        //controllerLeft = left.GetComponent<ActionBasedControllerManager>();
        Debug.Log("LEFT controller: " + controllerLeft.name);
        Debug.Log("RIGHT controller: " + controllerRight.name);
        //controllerRight = GameObject.Find("Right Controller").GetComponent<ActionBasedControllerManager>();


        if (controllerLeft != null && controllerRight != null)
        {
            if (moveType == false && handedness == false) // Smooth move & Right handed (Smooth locomotion control on Left hand and turn on right)
            {
                controllerLeft.smoothMotionEnabled = true;
                controllerRight.smoothMotionEnabled = false;
                controllerRight.transform.GetChild(3).GameObject().SetActive(false);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(false);

                if (turnType == false)
                {
                    controllerRight.smoothTurnEnabled = true;
                }
                else
                {
                    controllerRight.smoothTurnEnabled = false;
                }
            }

            if (moveType == true && handedness == false) // Teleport move & Right handed (Teleport locomotion control on Left hand and turn on right)
            {
                controllerLeft.smoothMotionEnabled = false;
                controllerRight.smoothMotionEnabled = false;
                controllerRight.transform.GetChild(3).GameObject().SetActive(false);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(true);

                if (turnType == true)
                {
                    controllerRight.smoothTurnEnabled = true;
                }
                else
                {
                    controllerRight.smoothTurnEnabled = false;
                }
            }

            if (moveType == false && handedness == true) // Smooth move & Left handed (Smooth locomotion control on Right hand and turn on right)
            {
                controllerLeft.smoothMotionEnabled = false;
                controllerRight.smoothMotionEnabled = true;
                controllerRight.transform.GetChild(3).GameObject().SetActive(false);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(false);

                if (turnType == false)
                {
                    controllerLeft.smoothTurnEnabled = true;
                }
                else
                {
                    controllerLeft.smoothTurnEnabled = false;
                }
            }

            if (moveType == true && handedness == true) // Teleport move & Left handed (Teleport locomotion control on Right hand and turn on right)
            {
                controllerLeft.smoothMotionEnabled = false;
                controllerRight.smoothMotionEnabled = false;
                controllerRight.transform.GetChild(3).GameObject().SetActive(true);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(false);

                if (turnType == true)
                {
                    controllerLeft.smoothTurnEnabled = true;
                }
                else
                {
                    controllerLeft.smoothTurnEnabled = false;
                }
            }

            Debug.Log("Left Controller: " + controllerLeft);
            Debug.Log("Right Controller: " + controllerRight);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (rig == null)
        {
            rig = GameObject.Find("XR Origin (XR Rig)");

            tunnelControl = GameObject.Find("TunnelingVignette");

            Debug.Log("Rig: " + rig);
            Debug.Log("Tunnel: " + tunnelControl);

            controllerLeft = rig.transform.GetChild(0).GetChild(3).GetComponent<ActionBasedControllerManager>();
            controllerRight = rig.transform.GetChild(0).GetChild(5).GetComponent<ActionBasedControllerManager>();

            Debug.Log("LEFT controller: " + controllerLeft.name);
            Debug.Log("RIGHT controller: " + controllerRight.name);
        }

        /*
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        if (inputDevices.Count > 2)
        {
            Debug.Log("Found One");
        }
        else
        {
            foreach (var device in inputDevices)
            {
                Debug.Log("Input Device: " + device.name + "Count: " + inputDevices.Count);
            }
        }*/

        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count == 1)
        {
            deviceLeft = leftHandDevices[0];
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceLeft.name, deviceLeft.role.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        if (rightHandDevices.Count == 1)
        {
            deviceRight = rightHandDevices[0];
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceRight.name, deviceRight.role.ToString()));
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one right hand!");
        }

        UpdateMove();
        UpdateTurn();
        UpdateTunneling();

        //Debug.Log("Tunneling: " + tunnelControl);
    }

    public void SetMove(TMP_Dropdown state) // 0 = Smooth   1 = Teleport
    {
        if (state.value == 0)
        {
            moveType = false;
        }
        else
        {
            moveType = true;
        }
    }

    public void SetTurn(TMP_Dropdown state) // 0 = Smooth   1 = Snap
    {
        if (state.value == 0)
        {
            turnType = false;
        }
        else
        {
            turnType = true;
        }
    }

    public void SetTurnVal(float value)
    {
        turnStrength = value;
    }

    public void SetTunneling(Toggle state) // 0 = Off   1 = On
    {
        tunneling = state.isOn;
    }

    public void SetTunnelingVal(Slider state)
    {
        tunnelStrength = state.value;
    }
    public void SetHandedness(TMP_Dropdown state) // 0 = Right   1 = Left
    {
        //handedness = hand;
        if (state.value == 0)
        {
            handedness = false;
        }
        else
        {
            handedness = true;
        }
    }

    void UpdateMove()
    {
        GameObject teleportObj = rig.transform.GetChild(1).GetChild(2).GameObject();

        //Debug.Log("Object: " + teleportObj);

        /*
        if (teleportObj.activeInHierarchy != moveType)
        {
            teleportObj.SetActive(moveType);

            if (moveType)
            {
                controllerLeft.smoothMotionEnabled = false;
                controllerRight.smoothMotionEnabled = false;

                if (handedness)
                {
                    controllerRight.transform.Find("Teleport Interactor").gameObject.SetActive(false);
                    controllerLeft.transform.Find("Teleport Interactor").gameObject.SetActive(true);
                }
                else
                {
                    controllerRight.transform.Find("Teleport Interactor").gameObject.SetActive(true);
                    controllerLeft.transform.Find("Teleport Interactor").gameObject.SetActive(false);
                }
            }
        }*/

        teleportObj.SetActive(moveType);

        if (moveType)
        {
            controllerLeft.smoothMotionEnabled = false;
            controllerRight.smoothMotionEnabled = false;

            if (handedness)
            {
                controllerRight.transform.GetChild(3).GameObject().SetActive(false);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(true);
            }
            else
            {
                controllerRight.transform.GetChild(3).GameObject().SetActive(true);
                controllerLeft.transform.GetChild(3).GameObject().SetActive(false);
            }
        }

        if (handedness)
        {
            controllerLeft.smoothMotionEnabled = false;
            controllerRight.smoothMotionEnabled = !moveType;
            controllerRight.transform.GetChild(3).GameObject().SetActive(false);
        }
        else
        {
            controllerRight.smoothMotionEnabled = false;
            controllerLeft.smoothMotionEnabled = !moveType;
            controllerLeft.transform.GetChild(3).gameObject.SetActive(false);
        }


        //Debug.Log("Controller: " + handedness);
    }

    void UpdateTurn()
    {
        if (handedness)
        {
            controllerLeft.smoothTurnEnabled = !turnType;
            controllerRight.smoothTurnEnabled = false;
        }
        else
        {
            controllerRight.smoothTurnEnabled = !turnType;
            controllerLeft.smoothTurnEnabled = false;
        }

    }

    void UpdateTunneling()
    {
        GameObject tunnelobj = rig.transform.GetChild(0).GetChild(0).GetChild(0).GameObject();
        //Debug.Log("Object: " + tunnelobj);

        if (tunnelobj.activeInHierarchy != tunneling)
        {
            tunnelControl.SetActive(tunneling);
        }


        float currentTunnelSize = tunnelControl.GetComponent<TunnelingVignetteController>().defaultParameters.apertureSize;
        if (currentTunnelSize != tunnelStrength)
        {
            //tunnelControl.GetComponent<TunnelingVignetteController>().currentParameters.apertureSize = tunnelStrength;
            tunnelControl.GetComponent<TunnelingVignetteController>().defaultParameters.apertureSize = tunnelStrength;
            Debug.Log("New Tunnel Size: " + tunnelStrength);
        }
    }

}


