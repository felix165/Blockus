using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGrids : MonoBehaviour
{
    public GameObject lineObj;
    public Vector2 range = new Vector2(162f, 162f);
    public Vector2 count = new Vector2(6, 6);
    // Start is called before the first frame update
    void Start()
    {
        generateGrid(range, count);
    }

    void generateGrid(Vector2 range, Vector2 count)
    {
        if(count.x > 2)
        {
            Instantiate(lineObj, new Vector2(0, 0), Quaternion.identity,transform);
            for (int i = 1; i <= count.x/2; i++)
            {
                Instantiate(lineObj, new Vector2(0, i * range.y), Quaternion.identity, transform);
                Instantiate(lineObj, new Vector2(0, -i * range.y), Quaternion.identity, transform);
            }
        }
        else
        {
            Instantiate(lineObj, new Vector2(0, range.y/2), Quaternion.identity, transform);
            Instantiate(lineObj, new Vector2(0, -range.y / 2), Quaternion.identity, transform);
        }

        if (count.y > 2)
        {
            Instantiate(lineObj, new Vector2(0, 0), Quaternion.Euler(0,0,90), transform);
            for (int i = 1; i <= count.x / 2; i++)
            {
                Instantiate(lineObj, new Vector2(i * range.y,0), Quaternion.Euler(0, 0, 90), transform);
                Instantiate(lineObj, new Vector2(-i * range.y,0), Quaternion.Euler(0, 0, 90), transform);
            }
        }
        else
        {
            Instantiate(lineObj, new Vector2(range.y / 2,0), Quaternion.Euler(0, 0, 90), transform);
            Instantiate(lineObj, new Vector2(-range.y / 2,0), Quaternion.Euler(0, 0, 90), transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
