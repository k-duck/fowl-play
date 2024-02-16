using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameController controller;
    private bool visible = false;
    public void StartGame()
    {
        SceneManager.LoadScene(1);

        /*
        var controller = new List<GameController>();
        controller = (GameController)FindObjectOfType(typeof(GameController));
        if (controller.Count > 1)
        {
            Debug.Log("Too many GameControllers");
        }
        else if(controller.Count > 0)
        {
            Debug.Log("GameController exists");
        }
        else
        {
            Debug.Log("GameController does not exists");
        }*/

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
}
