using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;

public class GameOverScript : MonoBehaviour
{

    [SerializeField] private Animator gameOverAnim;

    public GameObject DeadVignette;

    public static UnityEvent gameOverEvent = new UnityEvent();

    private GameObject rig;

    // Start is called before the first frame update
    void Start()
    {
        gameOverAnim.gameObject.SetActive(false);
        gameOverEvent.AddListener(GameOver);

        rig = GameObject.Find("XR Origin (XR Rig)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverAnim.gameObject.SetActive(true);

        //Show VR player they are dead
        DeadVignette.SetActive(true);

        GameObject smoothObj = rig.transform.GetChild(1).GetChild(1).GameObject();

        //this Sucks so much :((
        UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider.GravityApplicationMode referenceABCMPB = UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider.GravityApplicationMode.Immediately;

        smoothObj.GetComponent<ActionBasedContinuousMoveProvider>().gravityApplicationMode = referenceABCMPB;

        //Disable teleport when dead
        GameObject teleportObj = rig.transform.GetChild(1).GetChild(2).GameObject();
        teleportObj.SetActive(false);

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
