using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public GameObject settingsScreen;
    private GameController controller;
    private bool visible = false;

    private int movType_prev;
    private int turnType_prev;
    private int handType_prev;
    private bool tunnel_prev;
    private float tunnelVal_prev;

    public TMP_Dropdown movType_curr;
    public TMP_Dropdown turnType_curr;
    public TMP_Dropdown handType_curr;
    public Toggle tunnel_curr;
    public Slider tunnelVal_curr;

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

        turnType_curr.value = turnType_prev;
        handType_curr.value = handType_prev;
        tunnel_curr.isOn = tunnel_prev;
        tunnelVal_curr.value = tunnelVal_prev;

        Debug.Log("Controller: " + controller.name);
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
            visible = false;
        }else
        {
            settingsScreen.SetActive(true);
            visible = true;
        }
        
    }

    public void SetMove(TMP_Dropdown state) // 0 = Smooth   1 = Teleport
    {
        controller.SetMove(state);
    }

    public void SetTurn(TMP_Dropdown state) // 0 = Smooth   1 = Snap
    {
        controller.SetTurn(state);
    }

    public void SetTurnVal(float value)
    {
        controller.SetTurnVal(value);
    }

    public void SetTunneling(Toggle state) // 0 = Off   1 = On
    {
        controller.SetTunneling(state);
    }

    public void SetTunnelingVal(Slider state)
    {
        controller.SetTunnelingVal(state);
    }
    public void SetHandedness(TMP_Dropdown state) // 0 = Right   1 = Left
    {
        //handedness = hand;
        controller.SetHandedness(state);
    }
}
