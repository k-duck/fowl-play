using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameOverScript : MonoBehaviour
{

    [SerializeField] private Animator gameOverAnim;

    public static UnityEvent gameOverEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        gameOverAnim.gameObject.SetActive(false);
        gameOverEvent.AddListener(GameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverAnim.gameObject.SetActive(true);
        gameOverAnim.Play("GameOverFlashing");
        gameOverAnim.Play("GameOverJailBars");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
