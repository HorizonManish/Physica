using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Charge.cs of each charge is instantiating Ef lines at the start. 
 * This script has a public refernce of its line renderer component- "lineRenderer"
 * 
 */
public class ElectricLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int EFlinesLength = 10;
    public float lineSmootheness = 0.1f;
    public float chargeRadius;
    public Charge parentCharge;     //refernce taken just after instantiating by charge.cs

    // is charged system altered variables
    List<Vector3> chargePositionsList = new List<Vector3>();
    List<Vector3> tempChargePositionsList = new List<Vector3>();
    List<float> chargeList = new List<float>();
    List<float> tempChargeList = new List<float>();
    int tempChargeCount;

    private void Update()
    {
        if (lineSmootheness < 0) lineSmootheness = 0;
        UpdateEF();
    }

    void UpdateEF()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (parentCharge == null)       //it allows any efline to exist independently. we can study the nature EF in mid air
        {
            for (int i = 1; i < EFlinesLength; i++)
            {
              Vector3 position = lineRenderer.GetPosition(i - 1) + eFAtPoint(lineRenderer.GetPosition(i - 1)).normalized * lineSmootheness;
                foreach (var charge in Charge.Attractors)       //if ef line comes too near to any charge, stop making the field
                {
                    if (IsPointInsideASphere(position, charge.transform.position, chargeRadius))
                    {
                        return;     //exit the function
                    }
                }
                lineRenderer.positionCount = i + 1;
                lineRenderer.SetPosition(i, position);
            }
        }
        else                            
        {
            for (int i = 1; i < parentCharge.EFlinesLength; i++)
            {
                Vector3 position;
                if(parentCharge.charge >=0)
                {
                    position = lineRenderer.GetPosition(i - 1) + eFAtPoint(lineRenderer.GetPosition(i - 1)).normalized * (1/parentCharge.lineSmootheness);
                }
                else
                {
                    position = lineRenderer.GetPosition(i - 1) - eFAtPoint(lineRenderer.GetPosition(i - 1)).normalized *(1/ parentCharge.lineSmootheness);
                }
                
                foreach (var charge in Charge.Attractors)
                {
                    if (IsPointInsideASphere(position, charge.transform.position, parentCharge.chargeRadius))
                    {
                        return;
                    }
                }
                lineRenderer.positionCount = i + 1;
                lineRenderer.SetPosition(i, position);
            }
        }
        
        
    }
    bool IsPointInsideASphere(Vector3 point, Vector3 spherePos, float sphereRadius)
    {
        if ((point - spherePos).magnitude > sphereRadius)
        {
            return false;
        }
        else return true;
    }
    Vector3 eFAtPoint(Vector3 point)            //calculates EF at a particular point by taking data from static charge list
    {
        Vector3 electricfieldVector = Vector3.zero;
        if (Charge.Attractors != null)
        {            
            foreach (var charge in Charge.Attractors)
            {
                Vector3 distanceVector = point - charge.transform.position;
                if (distanceVector.magnitude != 0)
                {
                    float efMagnitude;
                    efMagnitude = charge.charge / Mathf.Pow(distanceVector.magnitude, 2);
                    electricfieldVector += efMagnitude * distanceVector.normalized;
                }
            }
        }
        return electricfieldVector;
    }
    bool IsChargeSystemAltered()
    {
        int chargeCount = Charge.Attractors.Count;
        bool isChargesAltered = true;
        if (tempChargeCount != chargeCount)
        {
            tempChargeCount = chargeCount;
            chargePositionsList.Clear();
            chargeList.Clear();
            tempChargePositionsList.Clear();
            tempChargeList.Clear();
            foreach (var charge in Charge.Attractors)
            {
                chargePositionsList.Add(charge.transform.position);
                tempChargePositionsList.Add(charge.transform.position);
                chargeList.Add(charge.charge);
                tempChargeList.Add(charge.charge);
            }
            return true;
        }
        else
        {
            for (int i = 0; i < chargeCount; i++)
            {
                chargePositionsList[i] = Charge.Attractors[i].transform.position;
                chargeList[i] = Charge.Attractors[i].charge;
            }
            for (int i = 0; i < chargeCount; i++)
            {
                if (chargePositionsList[i] != tempChargePositionsList[i] || chargeList[i] != tempChargeList[i])
                {
                    for (int j = 0; j < chargeCount; j++)
                    {
                        tempChargePositionsList[i] = chargePositionsList[i];
                        tempChargeList[i] = chargeList[i];
                    }
                    isChargesAltered = true;
                }
                else isChargesAltered = false;
            }
        }
        return isChargesAltered;
    }
}
