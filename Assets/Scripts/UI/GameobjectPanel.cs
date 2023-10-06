using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameobjectPanel : MonoBehaviour
{
    private static GameobjectPanel _instance;
    public static GameobjectPanel Instance
    {
        get
        {           
            return _instance;
        }
    }
    public GameObject UI;
    public GameObject ObjUIButtonPrefab;
    // Selected Object parts
    GameObject selectedObj;
    GameObject previousSelectedObj;

    public Camera main_camera;
    public RectTransform rectTransformOfCanvas;
    public GameObject buttonHolder;
    RectTransform UIRectTransform;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        UIRectTransform = UI.GetComponent<RectTransform>();
        ClosePanel();
    }
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            previousSelectedObj = selectedObj;
            selectedObj = GameManager.Instance.MouseClick();
            UI.SetActive(false);
        }
        if(Input.GetMouseButtonDown(1))
        {
            previousSelectedObj = selectedObj;
            selectedObj = GameManager.Instance.MouseClick();
            if(selectedObj !=null)
            {   
                if(selectedObj != previousSelectedObj)
                {
                    ClosePanel();
                    SendGameobjectPropertiesToButton(selectedObj);
                    UIRectTransform.anchoredPosition = MouseToCanvasPosition()- GetComponent<RectTransform>().anchoredPosition3D;
                    UI.SetActive(true);
                }
                else
                {
                    UIRectTransform.anchoredPosition = MouseToCanvasPosition()- GetComponent<RectTransform>().anchoredPosition3D;
                    UI.SetActive(true);
                }                
            }
            else if(selectedObj == null)
            {
                print("nothing is selected ");
                ClosePanel();
            }
        }
    }
    void SendGameobjectPropertiesToButton(GameObject obj)
    {
        GameObject ObjUIButton = Instantiate(ObjUIButtonPrefab);
        ObjUIButton.transform.SetParent(buttonHolder.transform);

        if (obj.GetComponent<MeshRenderer>().material!=null)
        {                   
            ObjUIButton.GetComponent<ObjUIButton>().MaterialPanel(obj.GetComponent<MeshRenderer>().material);
        }
        if(obj.GetComponent<Transform>()!= null)
        {

        }
        if(obj.GetComponent<Rigidbody>()!=null)
        {

        }
        if (obj.GetComponent<Collider>()!=null)
        {

        }              
    }
    
    public void ClosePanel()
    {
        foreach(Transform child in buttonHolder.transform)
        {
            Destroy(child.gameObject);
        }
        UI.SetActive(false);        
    }
    Vector3 MouseToCanvasPosition()
    {
        Vector3 viewportPoint = main_camera.ScreenToViewportPoint(Input.mousePosition);       
        Vector3 coordinates = new Vector3(rectTransformOfCanvas.rect.width * viewportPoint.x, rectTransformOfCanvas.rect.height * viewportPoint.y, 0);
        return coordinates;
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
