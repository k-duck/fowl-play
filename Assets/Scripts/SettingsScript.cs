using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject VRsettingsScreen;
    private GameController controller;
    private bool visible = false;

    public int movType_prev;
    private int turnType_prev;
    private int handType_prev;
    private bool tunnel_prev;
    private float tunnelVal_prev;

    public TMP_Dropdown movType_curr;
    public TMP_Dropdown turnType_curr;
    public TMP_Dropdown handType_curr;
    public Toggle tunnel_curr;
    public Slider tunnelVal_curr;

    public TMP_Dropdown VR_movType_curr;
    public TMP_Dropdown VR_turnType_curr;
    public TMP_Dropdown VR_handType_curr;
    public Toggle VR_tunnel_curr;
    public Slider VR_tunnelVal_curr;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        controller = objs[0].GetComponent<GameController>();

        movType_prev = controller.GetComponent<GameController>().GetMove();
        turnType_prev = controller.GetComponent<GameController>().GetTurn();
        handType_prev = controller.GetComponent<GameController>().GetHand();
        tunnel_prev = controller.GetComponent<GameController>().GetTunnel();
        tunnelVal_prev = controller.GetComponent<GameController>().GetTunnelVal();

        movType_curr.value = movType_prev;
        Debug.Log("Move current: " + movType_curr.value);
        Debug.Log("Move previous: " + movType_prev);

        //Set PC settings values
        turnType_curr.value = turnType_prev;
        handType_curr.value = handType_prev;
        tunnel_curr.isOn = tunnel_prev;
        tunnelVal_curr.value = tunnelVal_prev;
        
        //Set VR settings values
        VR_movType_curr.value = movType_prev;
        VR_turnType_curr.value = turnType_prev;
        VR_handType_curr.value = handType_prev;
        VR_tunnel_curr.isOn = tunnel_prev;
        VR_tunnelVal_curr.value = tunnelVal_prev;

        Debug.Log("Controller: " + controller.name);
    }

    void Update()
    {
        //sync the to UI interfaces

        if (movType_curr.value != movType_prev || VR_movType_curr.value != movType_prev)
        {
            movType_curr.value = movType_prev;

            VR_movType_curr.value = movType_prev;
        }

        if (turnType_curr.value != turnType_prev || VR_turnType_curr.value != turnType_prev)
        {
            turnType_curr.value = turnType_prev;

            VR_turnType_curr.value = turnType_prev;
        }

        if (handType_curr.value != handType_prev || VR_handType_curr.value != handType_prev)
        {
            handType_curr.value = handType_prev;

            VR_handType_curr.value = handType_prev;
        }

        if (tunnel_curr.isOn != tunnel_prev || VR_tunnel_curr.isOn != tunnel_prev)
        {
            tunnel_curr.isOn = tunnel_prev;

            VR_tunnel_curr.isOn = tunnel_prev;
        }

        if (tunnelVal_curr.value != tunnelVal_prev || VR_tunnelVal_curr.value != tunnelVal_prev)
        {
            tunnelVal_curr.value = tunnelVal_prev;

            VR_tunnelVal_curr.value = tunnelVal_prev;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void ShowSettings()
    {
        if (visible)
        {
            settingsScreen.SetActive(false);
            VRsettingsScreen.SetActive(false);
            visible = false;
        }else
        {
            settingsScreen.SetActive(true);
            VRsettingsScreen.SetActive(true);
            visible = true;
        }
        
    }

    public void SetMove(TMP_Dropdown state) // 0 = Smooth   1 = Teleport
    {
        controller.SetMove(state);
        movType_prev = state.value;
    }

    public void SetTurn(TMP_Dropdown state) // 0 = Smooth   1 = Snap
    {
        controller.SetTurn(state);
        turnType_prev = state.value;
    }

    public void SetTurnVal(float value)
    {
        controller.SetTurnVal(value);

    }

    public void SetTunneling(Toggle state) // 0 = Off   1 = On
    {
        controller.SetTunneling(state);
        tunnel_prev = state.isOn;
    }

    public void SetTunnelingVal(Slider state)
    {
        controller.SetTunnelingVal(state);
        tunnelVal_prev = state.value;
    }
    public void SetHandedness(TMP_Dropdown state) // 0 = Right   1 = Left
    {
        //handedness = hand;
        controller.SetHandedness(state);
        handType_prev = state.value;
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        controller.isPaused = true;
        controller.rig.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);
        controller.rig.transform.GetChild(0).GetChild(5).GetChild(2).gameObject.SetActive(false);
        ShowSettings();
        Debug.Log("Game is Paused");
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        controller.isPaused = false;
        controller.rig.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);
        controller.rig.transform.GetChild(0).GetChild(5).GetChild(2).gameObject.SetActive(true);
        ShowSettings();
        Debug.Log("Play Game");
    }

}
