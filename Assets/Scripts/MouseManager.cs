using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    
    float lastFrameTime;
    public GameObject arrow;
    public GameObject zAxis;
    Transform cameraTempTrans;

    void Start()
    {
        lastFrameTime = Time.realtimeSinceStartup;
    }
    private void LateUpdate()
    {
        var myDeltaTime = Time.realtimeSinceStartup - lastFrameTime;
        lastFrameTime = Time.realtimeSinceStartup;
    }
    void Update()
    {       
        
        if(GameManager.Instance.XYPlaneView ==true)
        {
            zAxis.SetActive(false);
            cameraTempTrans = transform;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else if (GameManager.Instance.XYPlaneView == false)
        {
            zAxis.SetActive(true);
          //  transform.rotation = cameraTempTrans.rotation;
        }
    }
    

    IEnumerator XYPlaneCameraMove()
    {
        if (GameManager.Instance.XYPlaneView == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else if (GameManager.Instance.XYPlaneView == false)
        {

        }
    }
}

