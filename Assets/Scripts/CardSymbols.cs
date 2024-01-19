using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSymbols : MonoBehaviour
{
    public GameObject keycards;

    public cardTexture card;

    // Start is called before the first frame update
    void Start()
    {
        keycards.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
