using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDPhaseSpace : MonoBehaviour
{
    public Rigidbody bodyRB;              //transform of body whose phase space we are making
    public Transform pointer;
    private Vector3 previousPosition;
    private float previousTime;
    public float xAxisScaling = 1;
    public float yAxisScaling = 1;

    public enum velocityComponent
    {
        xComponent,
        yComponent,
        zComponent
    }
    public velocityComponent selectedVelComp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pointer != null && bodyRB != null)
        {
            Updatepointer(pointer, bodyRB);
        }
        else Debug.Log("Pointer or body not assigned");     
    }

    void Updatepointer(Transform PenTip, Rigidbody TargetBody )                 //use global variable for scalings
    {
        switch (selectedVelComp)
        {
            case velocityComponent.xComponent:
                PenTip.position = new Vector3(TargetBody.transform.position.x * xAxisScaling, bodyRB.velocity.x * yAxisScaling, PenTip.position.z);
                break;
            case velocityComponent.yComponent:
                PenTip.position = new Vector3(TargetBody.transform.position.y * xAxisScaling, bodyRB.velocity.y * yAxisScaling, PenTip.position.z);
                break;
            case velocityComponent.zComponent:
                PenTip.position = new Vector3(TargetBody.transform.position.z * xAxisScaling, bodyRB.velocity.z * yAxisScaling, PenTip.position.z);
                break;
        }
        
    }
/*
    Vector3 VelFromTransform(Transform targetBody)
    {
        if (previousTime == 0f)
        {
            previousPosition = targetBody.position;
            previousTime = Time.time;
            return Vector3.zero;
        }

        // Calculate time elapsed since the last frame
        float currentTime = Time.time;
        float deltaTime = currentTime - previousTime;

        // Calculate the displacement vector between the current and previous frames
        Vector3 displacement = targetBody.position - previousPosition;

        // Calculate velocity by dividing displacement by time elapsed
        Vector3 velocity = displacement / deltaTime;

        // Update previous position and time
        previousPosition = targetBody.position;
        previousTime = currentTime;

        return velocity;
    }*/
}
