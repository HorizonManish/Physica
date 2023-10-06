using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolPanel : MonoBehaviour
{
    public string panelName;
    public Text panelNameText;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
