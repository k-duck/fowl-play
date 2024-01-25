using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DocumentControls : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject documentsWindow;
    public List<GameObject> documents;

    [SerializeField]  private Canvas canvas;
    [SerializeField] private RectTransform dragControls;
    private float maxDragX, maxDragY, minDragX, minDragY;

    private void Start()
    {
        maxDragX = canvas.GetComponentInChildren<RectTransform>().rect.xMax;
        maxDragY = canvas.GetComponentInChildren<RectTransform>().rect.yMax;
        minDragX = canvas.GetComponentInChildren<RectTransform>().rect.xMin;
        minDragY = canvas.GetComponentInChildren<RectTransform>().rect.yMin;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");    
        dragControls.anchoredPosition += eventData.delta / canvas.scaleFactor;
        KeepInBounds();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        KeepInBounds();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void KeepInBounds()
    {
        Debug.Log("Email Pos: " + dragControls.anchoredPosition);
        if (dragControls.anchoredPosition.x - dragControls.rect.xMax + 50 > maxDragX)
            dragControls.anchoredPosition = new Vector2(maxDragX + dragControls.rect.xMax - 50, dragControls.anchoredPosition.y);
        if (dragControls.anchoredPosition.y - dragControls.rect.yMax + 50 > maxDragY)
            dragControls.anchoredPosition = new Vector2(dragControls.anchoredPosition.x, maxDragY + dragControls.rect.yMax - 50);
        if (dragControls.anchoredPosition.x - dragControls.rect.xMin - 50 < minDragX)
            dragControls.anchoredPosition = new Vector2(minDragX + dragControls.rect.xMin + 50, dragControls.anchoredPosition.y);
        if (dragControls.anchoredPosition.y - dragControls.rect.yMin - 50 < minDragY)
            dragControls.anchoredPosition = new Vector2(dragControls.anchoredPosition.x, minDragY + dragControls.rect.yMin + 50);
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
