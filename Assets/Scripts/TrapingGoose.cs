using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapingGoose : MonoBehaviour
{

    public bool isGrooseTraped;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Goose"))
        {
            isGrooseTraped = true;
        }
        else
        {
            isGrooseTraped = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrooseTraped = false;
    }
}
