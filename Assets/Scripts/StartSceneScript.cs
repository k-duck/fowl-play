using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public GameObject settingsScreen;
    private GameController controller;
    private bool visible = false;
    public void StartGame()
    {
        SceneManager.LoadScene(1);

        controller = (GameController)FindObjectOfType(typeof(GameController));
        if (controller)
        {
            Debug.Log("GameController exists");
        }
        else
        {
            Debug.Log("GameController does not exists");
        }
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
        }else
        {
            settingsScreen.SetActive(true);
        }
        
    }
}
