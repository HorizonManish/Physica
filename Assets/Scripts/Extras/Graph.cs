using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class Graph : MonoBehaviour
{
    public Sprite circleSprite;
    private void Awake()
    {

    }
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
    }
}*/
public class Graph : MonoBehaviour
{
    public float distanceBetweenPoints = 0.1f;
    public Rigidbody rigidbodyComp;
    public float graphHeightScalar = 0.5f;
    public int pointsInGraph;
    float onYAxis;
    LineRenderer graphOutline;
    private void Start()
    {
        if(GetComponent<LineRenderer>() !=null)
        {
            graphOutline = GetComponent<LineRenderer>();           
        }
        else
        {
            graphOutline = gameObject.AddComponent<LineRenderer>();
        }
        graphOutline.positionCount = pointsInGraph;
        StartCoroutine(_MakeGraph());
        
        
    }
    private void Update()
    {
        onYAxis = rigidbodyComp.position.y * graphHeightScalar;             //use lerp to smooth out edges
    }

    IEnumerator _MakeGraph()
    {
        for (int i = 0; i < graphOutline.positionCount; i++)
        {
            graphOutline.SetPosition(i, new Vector3(i*distanceBetweenPoints, 0, 0));
        }
        while(true)
        {
            graphOutline.SetPosition(graphOutline.positionCount-1, new Vector3(graphOutline.GetPosition(graphOutline.positionCount-1).x, onYAxis, 0));
            yield return new WaitForSeconds(distanceBetweenPoints);
            for (int i = 0; i < graphOutline.positionCount-1; i++)
            {
                graphOutline.SetPosition(i, new Vector3(graphOutline.GetPosition(i).x, graphOutline.GetPosition(i + 1).y,0));
            }
            
        }
    }
}
