using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFieldArrow : MonoBehaviour
{
    Vector3 electricfieldVector;
    EFVectorField vectorFieldScript;
    private void Start()
    {
        vectorFieldScript = FindObjectOfType<EFVectorField>();
        electricfieldVector = Vector3.zero;
    }
    private void Update()
    {
        if(Charge.Attractors != null)
        {
            foreach (var charge in Charge.Attractors)
            {
                Vector3 distanceVector = transform.position - charge.transform.position;
                if (distanceVector.magnitude != 0)
                {
                    float efMagnitude;
                    if (vectorFieldScript.currentForceState == EFVectorField.forceDependecy.quadratic)
                    {
                        efMagnitude = vectorFieldScript.coulombConst * charge.charge / Mathf.Pow(distanceVector.magnitude, 2);
                    }
                    else
                    {
                        efMagnitude = vectorFieldScript.coulombConst * charge.charge / Mathf.Pow(distanceVector.magnitude, 1);
                    }

                    electricfieldVector += efMagnitude * distanceVector.normalized;
                }

            }
        }
        
        transform.up = electricfieldVector;
        transform.localScale = new Vector3(1, electricfieldVector.magnitude * vectorFieldScript.arrowScaler, 1);
        electricfieldVector = Vector3.zero;

    }
}
