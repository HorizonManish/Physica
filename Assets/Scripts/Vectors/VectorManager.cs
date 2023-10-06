using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorManager : MonoBehaviour
{
    private static VectorManager _instance;
    public static VectorManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("Vector Manager");
                go.AddComponent<VectorManager>();
            }
            return _instance;
        }
    }
    
    public GameObject resultantVector;
    public bool drawVectors = false;
    public bool showComponents = false;
    public float vectorScaling = 1;
    public LayerMask vectorLayer;

    ResultantVector rv;
    private bool buttonReleased;
    Vector3 tempDirection = new Vector3();
    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (drawVectors == true)
        {
            DrawVectorsFromMouse();
        }
        if(Input.GetMouseButtonDown(0))
        {
           // VectorSelector();
        }
        
    }
    public void DrawVectorsFromMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {               
                Debug.DrawRay(Vector3.zero, hit.point * 1000);
                tempDirection = hit.point;
                GameObject tempArrow = Instantiate(resultantVector, hit.point, Quaternion.identity);
                if(tempArrow.GetComponent<ResultantVector>()!=null)
                {
                    rv = tempArrow.GetComponent<ResultantVector>();
                }
                else
                {
                    Debug.Log("Resultant vector is not attached");
                }
                tempArrow.transform.parent = transform;           
                buttonReleased = false;

            }
        }
        if (Input.GetMouseButton(0))
        {
            if (buttonReleased == false)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(Vector3.zero, tempDirection);
                if (Physics.Raycast(ray, out hit,1000,~vectorLayer))
                {
                    Debug.DrawRay(Vector3.zero, hit.point);
                    rv.Vector3Input( hit.point - tempDirection);                
                    Debug.DrawRay(rv.transform.position, hit.point - tempDirection);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            buttonReleased = true;
            rv = null;
        }
    }
    public void VectorSelector()
    {
        GameManager.Instance.MouseClick();
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,1000,vectorLayer))
        {
            print("vector is hittedd");
        }
    }
}
