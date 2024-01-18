using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentControls : MonoBehaviour
{
    public GameObject documentsWindow;
    public List<GameObject> documents;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDocsWindow()
    {
        if (documentsWindow.activeSelf)
        {
            documentsWindow.SetActive(false);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        } else
        {
            documentsWindow.SetActive(true);
        }
    }

    public void openDocument(int docID)
    {
        closeDocs();
        documents[docID].SetActive(true);
    }

    void closeDocs()
    {
        foreach (GameObject document in documents)
        {
            document.SetActive(false);
        }
    }
}
