using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreOfMass : MonoBehaviour
{
    public Transform COM;
    public Transform[] transforms;
    private float totalMass = 0f;
    private Vector3 com;
  
    void Start()
    {    
        COM.gameObject.SetActive(true);
        FindTotalMass();       
    }

    void Update()
    {
        FindCOM();
        COM.position = com/totalMass;
        com = new Vector3(0, 0, 0);
           
    }
    private void FindTotalMass()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            totalMass += transforms[i].GetComponent<Rigidbody>().mass;
        }
    }
    private void FindCOM()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            com += transforms[i].position * transforms[i].GetComponent<Rigidbody>().mass;
        }
    }
    
}
