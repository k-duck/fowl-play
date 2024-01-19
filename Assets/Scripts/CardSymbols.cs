using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSymbols : MonoBehaviour
{
    GameObject keycard;

    public cardTexture card;

    // Start is called before the first frame update
    void Start()
    {
        keycard = gameObject.transform.GetChild(((int)card.KeyCard.y) - 1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if  (this.gameObject.activeInHierarchy )
        {
            keycard.SetActive(true);
        }
    }
}
