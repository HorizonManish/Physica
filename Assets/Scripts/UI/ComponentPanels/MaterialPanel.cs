using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaterialPanel : MonoBehaviour, IDragHandler
{
    RectTransform thisRectTransform;
    public Canvas canvas;
    Material selectedObjMaterial;
    void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
    }
    
    void Update()
    {
        
    }
    public void GetProperties(Material material)
    {
        selectedObjMaterial = material;
    }   
    public void ClosePanel()
    {
        if(!GameobjectPanel.Instance.UI.activeSelf)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        thisRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
