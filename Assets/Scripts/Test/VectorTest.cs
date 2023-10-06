using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    public float distance;
    public GameObject charge;
    public int numberOfCharges;
    public GameObject Vector;
    Rigidbody bodyRB;
    public ResultantVector vt;

    void Start()
    {
        float angle = (Mathf.PI * 2) / numberOfCharges;
        for (int i = 0; i < numberOfCharges; i++)
        { 
            GameObject obj = Instantiate(charge, new Vector3(distance * Mathf.Cos(i * angle), distance * Mathf.Sin( i * angle),5),Quaternion.identity);
            obj.GetComponent<Charge>().chargeType = Charge.ChargeTypeSelector.Continous;
        }
        
    }
    void Update()
    {
        
    }
}
