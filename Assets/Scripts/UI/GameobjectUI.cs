using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameobjectUI : MonoBehaviour, IDragHandler
{
    RectTransform thisRectTransform;
    public Canvas canvas;
    private void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        thisRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
