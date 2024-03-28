using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleseTheGoose : MonoBehaviour
{
    public GameObject GooseItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ShowTheGoose()
    {
        yield return new WaitForSeconds(5);
        GooseItem.gameObject.SetActive(true);
    }
}
