using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisManager : MonoBehaviour
{
    public bool drawAxis = true;
    public GameObject xsubAxisPrefab;
    public GameObject ysubAxisPrefab;
    private List<GameObject> xSubAxis = new List<GameObject>();
    private List<GameObject> ySubAxis = new List<GameObject>();
    public float axisCoverage = 100f;
    private float tempCoverage;
    public float axisSpacing = 0.5f;
    private float tempSpacing;
    public GameObject xyPlane;  
    public Material lineMat;
    public GameObject axisSceneView;
  
    void Start()
    {
        axisSceneView.SetActive(false);
        tempCoverage = axisCoverage;
        tempSpacing = axisSpacing;
        if(drawAxis == true)
        {
            for (int i = 1; i <= axisCoverage / (2 * axisSpacing); i++)
            {
                GameObject positiveX = Instantiate(xsubAxisPrefab, new Vector3(axisSpacing * i, 0, 0), Quaternion.identity, xyPlane.transform) as GameObject;
                positiveX.transform.localScale = new Vector3(positiveX.transform.localScale.x, positiveX.transform.localScale.y, axisCoverage/2);
                xSubAxis.Add(positiveX);

                GameObject negativeX = Instantiate(xsubAxisPrefab, new Vector3(-axisSpacing * i, 0, 0), Quaternion.identity, xyPlane.transform) as GameObject;
                negativeX.transform.localScale = new Vector3(negativeX.transform.localScale.x, negativeX.transform.localScale.y, axisCoverage/2);
                xSubAxis.Add(negativeX);

                GameObject positiveY = Instantiate(ysubAxisPrefab, new Vector3(0, 0, axisSpacing * i), Quaternion.identity, xyPlane.transform) as GameObject;
                positiveY.transform.localScale = new Vector3(axisCoverage/2, positiveY.transform.localScale.y, positiveY.transform.localScale.z);
                ySubAxis.Add(positiveY);

                GameObject negativeY = Instantiate(ysubAxisPrefab, new Vector3(0, 0, -axisSpacing * i), Quaternion.identity, xyPlane.transform) as GameObject;
                negativeY.transform.localScale = new Vector3(axisCoverage/2, negativeY.transform.localScale.y, negativeY.transform.localScale.z);
                ySubAxis.Add(negativeY);
            }
        }
    }

    void Update()
    {
        if(drawAxis == true)
        {
            xyPlane.SetActive(true);
            if (tempSpacing != axisSpacing || tempCoverage != axisCoverage)
            //  if(axisCoverage<tempCoverage-0.5f || axisCoverage < tempCoverage + 0.5f || axisSpacing < tempSpacing - 0.5f || axisSpacing < tempSpacing + 0.5f)
            {
                for (int i = 0; i < xSubAxis.Count; i++)
                {
                    Destroy(xSubAxis[i]);
                }
                for (int i = 0; i < ySubAxis.Count; i++)
                {
                    Destroy(ySubAxis[i]);
                }
                xSubAxis.Clear();
                ySubAxis.Clear();
                for (int i = 1; i <= axisCoverage / (2 * axisSpacing); i++)
                {
                    GameObject positiveX = Instantiate(xsubAxisPrefab, new Vector3(axisSpacing * i, 0, 0), Quaternion.identity, xyPlane.transform) as GameObject;
                    positiveX.transform.localScale = new Vector3(positiveX.transform.localScale.x, positiveX.transform.localScale.y, axisCoverage / 2);
                    xSubAxis.Add(positiveX);

                    GameObject negativeX = Instantiate(xsubAxisPrefab, new Vector3(-axisSpacing * i, 0, 0), Quaternion.identity, xyPlane.transform) as GameObject;
                    negativeX.transform.localScale = new Vector3(negativeX.transform.localScale.x, negativeX.transform.localScale.y, axisCoverage / 2);
                    xSubAxis.Add(negativeX);

                    GameObject positiveY = Instantiate(ysubAxisPrefab, new Vector3(0, 0, axisSpacing * i), Quaternion.identity, xyPlane.transform) as GameObject;
                    positiveY.transform.localScale = new Vector3(axisCoverage / 2, positiveY.transform.localScale.y, positiveY.transform.localScale.z);
                    ySubAxis.Add(positiveY);

                    GameObject negativeY = Instantiate(ysubAxisPrefab, new Vector3(0, 0, -axisSpacing * i), Quaternion.identity, xyPlane.transform) as GameObject;
                    negativeY.transform.localScale = new Vector3(axisCoverage / 2, negativeY.transform.localScale.y, negativeY.transform.localScale.z);
                    ySubAxis.Add(negativeY);
                }
                tempCoverage = axisCoverage;
                tempSpacing = axisSpacing;
            }
        }
        else if(drawAxis == false)
        {
            xyPlane.SetActive(false);
        }

        
    }
    
}

