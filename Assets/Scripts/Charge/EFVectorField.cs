using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFVectorField : MonoBehaviour
{
    public GameObject EFArrowPrefab;
    public float coulombConst = 10f;
    public float arrowScaler = 1;
    public int fieldBoxXSize;
    public int fieldBoxYSize;
    public int fieldBoxZSize;
    public float vectorSpacing;
    List<GameObject> arrowList = new List<GameObject>();
    public enum forceDependecy
    {
        linear,
        quadratic
    }
    public forceDependecy currentForceState = forceDependecy.quadratic;

    int tempXSize;
    int tempYSize;
    int tempZSize;
    float tempVectorSpacing;

    private void Start()
    {
 
    }
    private void Update()
    {
        if(tempXSize!=fieldBoxXSize || tempYSize !=fieldBoxYSize || tempZSize != fieldBoxZSize || tempVectorSpacing!= vectorSpacing)
        {
            DestroyAllarrows();
            for (int i = 0; i < fieldBoxXSize; i++)
            {
                for (int j = 0; j < fieldBoxYSize ; j++)
                {
                    for (int k = 0; k < fieldBoxZSize; k++)
                    {
                        GameObject arrow = Instantiate(EFArrowPrefab, transform.position + new Vector3(vectorSpacing * i, vectorSpacing * j, vectorSpacing * k), Quaternion.identity);
                        arrow.transform.parent = transform;
                        arrowList.Add(arrow);
                    }
                }
            }          
            tempVectorSpacing = vectorSpacing;
            tempXSize = fieldBoxXSize;
            tempYSize = fieldBoxYSize;
            tempZSize = fieldBoxZSize;
        }
    }

    void DestroyAllarrows()
    {
        foreach (var arrow in arrowList)
        {
            Destroy(arrow);
        }
        arrowList.Clear();
    }
}
