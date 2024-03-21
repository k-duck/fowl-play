using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CageScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlaceholderGoose")
        {
            GooseAIScript.captureEvent.Invoke();
        }
    }
}
