using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainArrow : MonoBehaviour
{
   // public GameObject Container;
    public GameObject xComp;
    public GameObject yComp;
    public GameObject zComp;  
    public bool showComponentsLocal;
    [HideInInspector] public Vector3 mainVector;


    private void Start()
    {
        mainVector = new Vector3(1, 1, 1);
    }

    public void RecieveMainVector(Vector3 mv)
    {
        mainVector = mv;

    }
    private void OnEnable()
    {
        InstantiateComponents();
    }
   
    void Update()
    {
        MainVectorBehaviour();
        ComponentsBehaviour(); 
    }
    void MainVectorBehaviour()
    {
        transform.right = mainVector;
        transform.localScale = new Vector3(mainVector.magnitude * VectorManager.Instance.vectorScaling, transform.localScale.y, transform.localScale.z);
    }
    void InstantiateComponents()
    {
     /*   xComp = Instantiate(xCompPrefab, transform.position, Quaternion.identity);
        xComp.transform.parent = Container.transform;
        yComp = Instantiate(yCompPrefab, transform.position, Quaternion.identity);
        yComp.transform.parent = Container.transform;
        zComp = Instantiate(zCompPrefab, transform.position, Quaternion.identity);
        zComp.transform.parent = Container.transform;*/
    }
    void ComponentsBehaviour()
    {
        if(VectorManager.Instance.showComponents == true)
        {
            xComp.SetActive(true);
            yComp.SetActive(true);
            zComp.SetActive(true);
            float xAngle = Mathf.Deg2Rad * Vector3.Angle(transform.right, Vector3.right);
            xComp.transform.localScale = new Vector3(transform.localScale.x * Mathf.Cos(xAngle), xComp.transform.localScale.y, xComp.transform.localScale.z);
            xComp.transform.rotation = Quaternion.identity;

            float yAngle = Mathf.Deg2Rad * Vector3.Angle(transform.right, Vector3.forward);
            yComp.transform.localScale = new Vector3(yComp.transform.localScale.x, yComp.transform.localScale.y, transform.localScale.x * Mathf.Cos(yAngle));
            yComp.transform.rotation = Quaternion.identity;

            float zAngle = Mathf.Deg2Rad * Vector3.Angle(transform.right, Vector3.up);
            zComp.transform.localScale = new Vector3(zComp.transform.localScale.x, transform.localScale.x * Mathf.Cos(zAngle), zComp.transform.localScale.z);
            zComp.transform.rotation = Quaternion.identity;
        }
        else
        {
            xComp.SetActive(false);
            yComp.SetActive(false);
            zComp.SetActive(false);
        }
        
    }
}
