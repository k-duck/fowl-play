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

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        controller = objs[0].GetComponent<GameController>();
        Debug.Log("Controller: " + controller.name);
    }

    public void ExitGame()
    {
        Application.Quit();
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
