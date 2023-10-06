using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{  
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    
    [Header("Camera")]
    public bool XYPlaneView = false;
    
    [Header("--------")]
    GameObject selectedGameobject;
    GameObject tempGameobject ;
    Material selectedmaterial;
    Material tempmaterial;
    public float selectionBrightness = 0.5f;

    public GameObject tracer;
    //time
    [HideInInspector] public bool slowMotionIsOn;
    public float timeSpeed=0.5f;
    bool gameIsPaused = false;

    private void Awake()
    {
        _instance = this;
     //   controls.General.Pause.performed += ctx => PauseGame();
        slowMotionIsOn = false;
    }

    private void Update()
    {     
        SlowMotion();
        if(Input.GetKeyDown(KeyCode.P))
        {
            print("Pause is called");
            PauseGame();
        }
        if(Input.GetMouseButtonDown(0))
        {
            MouseClick();           
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            print("M key is pressed");
            StopOrMoveRigidbody(selectedGameobject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(selectedGameobject);
        }
    }

    public void AddTracer()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject trac = Instantiate(tracer, hit.point, Quaternion.identity) as GameObject;
            trac.transform.parent = hit.collider.gameObject.transform;
        }
    }
    public void SlowMotion()
    {
      if(slowMotionIsOn)
        {
            Time.timeScale = timeSpeed;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

        }
      else if(!slowMotionIsOn)
        {
            if(!gameIsPaused) Time.timeScale = 1;
            else Time.timeScale = 0;
            Time.fixedDeltaTime = 0.02f;
        }
    }
    public void SlowMotionIsOn()
    {
        print("it is called");
        slowMotionIsOn = !slowMotionIsOn;
    }
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            Time.timeScale = 0;
        }
        else if (gameIsPaused == false)
        {
            Time.timeScale = 1;
        }
    }
    public GameObject MouseClick()
    {     
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            tempGameobject = null;
            tempGameobject = selectedGameobject;

            tempmaterial = null;
            tempmaterial = selectedmaterial;
            if (tempGameobject!=null && tempGameobject.GetComponent<MeshRenderer>() != null )
            {
                tempGameobject.GetComponent<MeshRenderer>().material.color = new Color(tempmaterial.color.r - selectionBrightness, tempmaterial.color.g - selectionBrightness, tempmaterial.color.b - selectionBrightness);
            }

            selectedGameobject = null;
            selectedGameobject = hit.collider.gameObject;

            if (selectedGameobject.GetComponent<MeshRenderer>() != null)
            {      
                selectedmaterial = selectedGameobject.GetComponent<MeshRenderer>().material;
                selectedGameobject.GetComponent<MeshRenderer>().material.color = new Color(selectedmaterial.color.r + selectionBrightness, selectedmaterial.color.g + selectionBrightness, selectedmaterial.color.b + selectionBrightness);
            }
            else
            {
                tempmaterial = selectedmaterial;
                if (tempGameobject != null && tempGameobject.GetComponent<MeshRenderer>() != null)
                {
                    tempGameobject.GetComponent<MeshRenderer>().material.color = new Color(tempmaterial.color.r - selectionBrightness, tempmaterial.color.g - selectionBrightness, tempmaterial.color.b - selectionBrightness);
                }
            }
        }
        else
        {
            if(selectedGameobject!=null)
            {
                tempGameobject = selectedGameobject;
                tempmaterial = selectedmaterial;
                if (tempGameobject != null && tempGameobject.GetComponent<MeshRenderer>() != null)
                {
                    tempGameobject.GetComponent<MeshRenderer>().material.color = new Color(tempmaterial.color.r - selectionBrightness, tempmaterial.color.g - selectionBrightness, tempmaterial.color.b - selectionBrightness);
                }
                selectedGameobject = null;
            }
        }
       // print(selectedGameobject.name + " is hitted");
        return selectedGameobject;
    }
    public void StopOrMoveAllRigidBodies()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach(var rigidbody in rigidbodies)
        {
            if(rigidbody.isKinematic == false)
            {
                rigidbody.isKinematic = true;
            }
        }
    }
    public void StopOrMoveRigidbody(GameObject obj)
    {
        if(obj !=null)
        {
            Rigidbody rb;
            if (obj.GetComponent<Rigidbody>() != null)
            {
                rb = obj.GetComponent<Rigidbody>();
                if (rb.isKinematic == true)
                {
                    rb.isKinematic = false;
                }
                else
                {
                    rb.isKinematic = true;
                }
            }
            else
            {
                Debug.Log("Object has no rigidbody, can't be stopped.");
            }
        }
        else
        {
            Debug.Log("Nothing is selected");
        }
    }
    /* private void OnDrawGizmos()
    {
        for (int i = 0; i < axisCoverage/(2*axisSpacing); i++)      //XY Plane
        {
            Gizmos.DrawLine(new Vector3(axisCoverage/2, axisSpacing * i, 0), new Vector3(-axisCoverage/2, axisSpacing * i, 0));
            Gizmos.DrawLine(new Vector3(axisCoverage/2, -axisSpacing * i, 0), new Vector3(-axisCoverage/2, -axisSpacing * i, 0));

            Gizmos.DrawLine(new Vector3( axisSpacing * i, axisCoverage / 2, 0), new Vector3( axisSpacing * i, -axisCoverage / 2, 0));
            Gizmos.DrawLine(new Vector3( -axisSpacing * i, axisCoverage / 2, 0), new Vector3( -axisSpacing * i, -axisCoverage / 2, 0));
        }
    }*/
}

