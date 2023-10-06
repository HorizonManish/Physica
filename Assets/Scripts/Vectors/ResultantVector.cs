using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultantVector : MonoBehaviour
{
    public MainArrow mainArrow;
    Rigidbody bodyRB;
    Vector3 arrow;

    public void Vector3Input(Vector3 vector)
    {
        arrow = vector;
    }
    void Update()
    {
        if (bodyRB != null)
        {
            arrow = bodyRB.velocity;
        }
        
        mainArrow.RecieveMainVector(arrow);     //This arrow should contain the magnitude and the direction of vector to visualize
    }
    public ResultantVector InstantiateVectorType(GameObject go)
    {
        GameObject gameObject = Instantiate(this.gameObject, go.transform.position, Quaternion.identity);
        gameObject.transform.parent = go.transform;
        return this;
    }
  
    public void VelocityVisualizer(Rigidbody rb)
    {
        bodyRB = rb;
    }
    
}
