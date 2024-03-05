using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public ElevatorDoors Doors;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player enter");
            StartCoroutine(ExitScene());
        }
    }

    public IEnumerator ExitScene()
    {
        Doors.TriggerDoors();
        
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

}
