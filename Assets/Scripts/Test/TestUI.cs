using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    RectTransform rectTransform;
    public float Xcomp;
    public float Ycomp;
    public float Zcomp;
    // Start is called before the first frame update
    void Start()
    {
         rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition= new Vector3(Xcomp, Ycomp, Zcomp) ;
       //rectTransform.position = new Vector3(Xcomp, Ycomp, Zcomp);
      //transform.position = new Vector3(Xcomp, Ycomp, Zcomp);
      
        print("transform position is " + transform.position);
        print("rect position is " + rectTransform.position);

    }
}
