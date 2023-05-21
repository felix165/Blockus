using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int size = 1;
    public Transform[] corners = new Transform[4];
    private bool isPlaced = false; //? can't moved , can moved
    private bool isSelected = false;

    public float speed = 12f;
    private Vector3 startPos;
    private Rigidbody2D rb;
    public GameObject shadow;
    public Grid grid;

    public UnityEvent onMoveFail;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rightRotate();
                Debug.Log("LeftClick");
            }
        }

    }
    /// <summary>
    /// ubah
    /// </summary>
    private void OnMouseDown()
    {
        if (GameManager.isGamePause == true) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            startPos = this.transform.position;
            Debug.Log("Rightclickx");
            isSelected= true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    private void rightRotate()
    {
        transform.Rotate(0, 0, -90f);
    }


    private void OnMouseDrag()
    {
        if (GameManager.isGamePause == true) { return; }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.x = (mousePos.x - Screen.width / 2)/(Screen.height / 10);
            mousePos.y = (mousePos.y - Screen.height / 2)/(Screen.height / 10);
            rb.velocity = new Vector3((mousePos.x - (this.gameObject.transform.position.x)) * speed, (mousePos.y - (this.gameObject.transform.position.y)) * speed, 0);
            var shadowPos = PosAdjustment();
            shadowPos.z = 1f;
            shadow.transform.position = shadowPos;
        }
    }
    private void OnMouseUp()
    {
        if (GameManager.isGamePause == true) { return; }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 newPos = PosAdjustment();
            this.gameObject.transform.position = newPos;
            newPos.z = 1f;
            shadow.transform.position = newPos;
            if (startPos != this.transform.position)
            {
                isPlaced= true;
            }
            else
            {
                onMoveFail?.Invoke();
                this.transform.position = startPos;
            }
            //if(cant placed) -> destroy
            rb.velocity = new Vector3(0,0,0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isSelected = false;
        }
    }

    private Vector3 PosAdjustment()
    {
        Vector3Int gridPosition = grid.WorldToCell((this.gameObject.transform.position) + new Vector3(0.325f, 0.325f, 0f));        
        Vector3 goalPos = grid.CellToWorld(gridPosition);
        return goalPos;
    }





    
}
