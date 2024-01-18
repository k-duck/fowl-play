using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial_Floor_02");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
