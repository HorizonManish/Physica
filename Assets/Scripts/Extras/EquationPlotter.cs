using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationPlotter : MonoBehaviour
{
    public int n;

    public float graphDomainMinValue = -5f;
    private float prevGraphDomainMinValue = 0;
    public float graphDomainMaxValue = 5f;
    private float prevGraphDomainMaxValue = 0;
    public float distBetweenPoints = 0.1f;
    private float prevDistBetweenPoints = 0;

    private LineRenderer _curveLineRenderer;
    public bool showAxis = false;
    private bool _valuesAreChanged = true;
    public List<Vector3> curvePoints;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<LineRenderer>() != null)
        {
            _curveLineRenderer = GetComponent<LineRenderer>();
        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        if (showAxis == true)
        {

        }
        _valuesAreChanged = CheckIfValuesAreChanged();
        if (_valuesAreChanged == true)
        {
            FillpointsInList(curvePoints, graphDomainMinValue, graphDomainMaxValue, distBetweenPoints);
            PlotPoints(_curveLineRenderer, curvePoints);
            prevGraphDomainMinValue = graphDomainMinValue;
            prevGraphDomainMaxValue = graphDomainMaxValue;
            prevDistBetweenPoints = distBetweenPoints;
            _valuesAreChanged = false;
            print("Graph making func is running ");
        }
        else return;
        if (distBetweenPoints < 0) distBetweenPoints = 0.003f;
        else return;
    }

    void PlotPoints(LineRenderer p_curveLineRenderer, List<Vector3> p_curvePoints)   //will 
    {
        p_curveLineRenderer.positionCount = p_curvePoints.Count;
        for (int i = 0; i < p_curveLineRenderer.positionCount; i++)
        {
            p_curveLineRenderer.SetPosition(i, p_curvePoints[i]);
        }
    }

    void FillpointsInList(List<Vector3> p_curvePoints,float p_graphDomainMinValue, float p_graphDomainMaxValue, float p_distBetweenPoints)
    {
        int noOfPoints = Mathf.Abs(Mathf.RoundToInt((p_graphDomainMaxValue - p_graphDomainMinValue) / p_distBetweenPoints));
        p_curvePoints.Clear();
        for (int i = 0; i < noOfPoints; i++)
        {
            p_curvePoints.Add(new Vector3(p_graphDomainMinValue + i * distBetweenPoints, FunctionOutput(p_graphDomainMinValue + i * distBetweenPoints), 0));
        }
    }

    public float FunctionOutput(float x)
    {
        float outputValue = Mathf.Pow(x,n);                 //equation whose graph is going to plot
        return outputValue;
    }           //script plots the equation which this function has.

    bool CheckIfValuesAreChanged()
    {
        if (prevDistBetweenPoints != distBetweenPoints || prevGraphDomainMinValue != graphDomainMinValue || prevGraphDomainMaxValue != graphDomainMaxValue)
        {
            return true;
        }
        else return false;
    }
}
    
