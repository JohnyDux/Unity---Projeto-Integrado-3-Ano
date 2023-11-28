using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAction : MonoBehaviour
{
    public SpriteRenderer color;
    public Color StartColor;
    public Color ClickColor;
    public bool isHit;

    void Start()
    {
        isHit = false;
        color.color = StartColor;
    }


    void Update()
    {

    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            color.color = ClickColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "border")
        {
            if (color.color == ClickColor)
            {
                Debug.Log("you're fine");
            }
            else
            {
                Debug.Log("game has ended");
            }
        }
    }
}
