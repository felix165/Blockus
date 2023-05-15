using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGrids : MonoBehaviour
{
    public GameObject lineObj;
    public Vector2 range = new Vector2(162f, 162f);
    public Vector2 count = new Vector2(6, 6);
    private Vector2 oriScale;
    // Start is called before the first frame update
    void Start()
    {
        oriScale= lineObj.transform.localScale;
        generateGrid(range, count);
    }

    void generateGrid(Vector2 range, Vector2 count)
    {
        lineObj.transform.localScale = new Vector2(count.x * range.x, oriScale.y);
        if (count.x % 2 == 0)
        {
            Instantiate(lineObj, new Vector2(0, 0), Quaternion.identity, transform);
            for (int i = 1; i <= count.x / 2; i++)
            {
                Instantiate(lineObj, new Vector2(0, i * range.y), Quaternion.identity, transform);
                Instantiate(lineObj, new Vector2(0, -i * range.y), Quaternion.identity, transform);
            }
        }
        else
        {
            for (int i = 0; i <= count.x / 2; i++)
            {
                Instantiate(lineObj, new Vector2(0, (i * range.y) + ((float)range.y / 2f)), Quaternion.identity, transform);
                Instantiate(lineObj, new Vector2(0, -(i * range.y) - ((float)range.y / 2f)), Quaternion.identity, transform);
            }
        }

        lineObj.transform.localScale = new Vector2(count.y * range.y, oriScale.y);
        //lineObj.transform.localScale = new Vector2(oriScale.x, size.y * range.y);
        if (count.y % 2 == 0)
        {
            Instantiate(lineObj, new Vector2(0, 0), Quaternion.Euler(0, 0, 90), transform);
            for (int i = 1; i <= count.y / 2; i++)
            {
                Instantiate(lineObj, new Vector2(i * range.y, 0), Quaternion.Euler(0, 0, 90), transform);
                Instantiate(lineObj, new Vector2(-i * range.y, 0), Quaternion.Euler(0, 0, 90), transform);
            }
        }
        else
        {
            for (int i = 0; i <= count.y / 2; i++)
            {
                Instantiate(lineObj, new Vector2((i * range.y) + ((float)range.y / 2f), 0), Quaternion.Euler(0, 0, 90), transform);
                Instantiate(lineObj, new Vector2(-(i * range.y) - ((float)range.y / 2f), 0), Quaternion.Euler(0, 0, 90), transform);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
