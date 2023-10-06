using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{   
    [HideInInspector] public List<ResultantVector> resultantVectors;

    //Charge
    
    public enum ChargeTypeSelector
    {
        Discrete,
        Continous
    }
    [Header("Charge")]
    public ChargeTypeSelector chargeType;
    public float charge = 1f;
    public Rigidbody rb;                       //Reference of its own rigidbody component
    const float coloumbConstant = 9 ;       //Its actually 9*10^9
    public static List<Charge> Attractors;  //its a imp list, all the other scripts access this list to acquire info about the charges.
    int j = 0;
    public float chargeRadius = 0.2f;

    //electric lines
    [Header("EF lines")]
    public bool enableEF = true;
    public int eFLineCount = 15;
    public int EFlinesLength = 50;
    public float lineSmootheness = 0.1f;
    List<GameObject> electricLineList = new List<GameObject>(); //
    
    [Header("Prefabs")]
    //prefabs
    public GameObject Vector;
    public GameObject electricLinePrefab;



    private void Start()
    {
        if (chargeType == ChargeTypeSelector.Continous)    //Becuase I want to stop all the charges which has a continous tag(enum)
        {
            rb.isKinematic = true;
        }
        if (enableEF == true)
        {
            //EFin2dMaker();
            EFin3dMaker();
        }
        else return;
        
    }
    
    private void OnEnable()
    {       
        if (Attractors == null)
        {
            Attractors = new List<Charge>();
        }
        for (int i = 0; i < Attractors.Count; i++)
        {
            GameObject obj = Instantiate(Vector, transform.position, Quaternion.identity);
            obj.transform.parent = transform;                           //Due to this line main arrows' direction have little bit error
            if (obj.GetComponent<ResultantVector>()!=null)
            {
                resultantVectors.Add(obj.GetComponent<ResultantVector>());
            }
            else
            {
                Debug.Log("Resultant Vector of " + transform.name + " is not added in the list");
            }
        }
        EveryoneAddOneMoreForceVector();
        Attractors.Add(this);

    }
    private void FixedUpdate()
    {
        j = 0;
        foreach (Charge attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }
    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract (Charge objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;
        float chargeOfBody = objToAttract.charge;

        Vector3 direction = transform.position - rbToAttract.position;
        float distance = direction.magnitude;
        if (distance == 0)
        {
            return;
        }
        float forceMagnitude = -coloumbConstant * (charge * chargeOfBody) / Mathf.Pow(distance, 2);
     //   print("Force on " + this.name + " is " + forceMagnitude);
        Vector3 force = direction.normalized * forceMagnitude;
       //----------------------------------
        if(chargeType == ChargeTypeSelector.Continous && objToAttract.chargeType == ChargeTypeSelector.Continous)
        {
            resultantVectors[j].gameObject.SetActive(false);
        }
        else
        {
            resultantVectors[j].gameObject.SetActive(true);
            /*There is negative on force because this force attracting another body but here we are using it for the vector of this body*/
            resultantVectors[j].Vector3Input(-force);
            resultantVectors[j].transform.position = transform.position;
        }
        
        j++;

        rbToAttract.AddForce(force);    
        
    }
    public void EveryoneAddOneMoreForceVector()
    {
        foreach (Charge attractor in Attractors)
        {
            if (attractor != null)
            {
                GameObject obj = Instantiate(Vector, transform.position, Quaternion.identity);
                obj.transform.parent = attractor.transform;             //Due to this line main arrows' direction have little bit error
                if (obj.GetComponent<ResultantVector>() != null)
                {
                    attractor.resultantVectors.Add(obj.GetComponent<ResultantVector>());
                }
                else
                {
                    Debug.Log("Resultant Vector of " + transform.name + " is not added in the list");
                }
            }
        }
    }
    void EFin2dMaker()
    {
        // EF lines for 2d
        float theta = (2 * Mathf.PI) / eFLineCount; //angle between two EF lines
        for (int i = 0; i < eFLineCount; i++)       //15 is the no of EF lines in the starting 
        {
            GameObject efline = Instantiate(electricLinePrefab, transform.position + new Vector3(chargeRadius * Mathf.Cos(theta * i), chargeRadius * Mathf.Sin(theta * i), 0), Quaternion.identity);
            efline.transform.parent = transform;
            electricLineList.Add(efline);
            efline.GetComponent<ElectricLine>().parentCharge = this;
        }
    }
    void EFin3dMaker()
    {
        Vector3[] pts = pointsOnSphere(eFLineCount);
        // Ef Lines for 3d
           for (int i = 0; i < eFLineCount; i++)
           {
               GameObject efline = Instantiate(electricLinePrefab, transform.position + pointsOnSphere(eFLineCount)[i] * chargeRadius, Quaternion.identity);
               efline.transform.parent = transform;
               electricLineList.Add(efline);
               efline.GetComponent<ElectricLine>().parentCharge = this;
           }
    }
    Vector3[] pointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z));
        }
        Vector3[] pts = upts.ToArray();
        return pts;
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        foreach (var attractor in Attractors)
        {
            if (attractor != this)
            {
                Gizmos.DrawLine(transform.position, attractor.transform.position);
            }
        }
     /*   for (int i = 0; i < pointCount; i++)
        {
            Vector3 temp = pointsOnSphere(pointCount)[i];
            print(temp);
            Gizmos.DrawSphere(temp, 0.1f);
        }*/

    }



}
