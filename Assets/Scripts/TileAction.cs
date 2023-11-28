using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAction : MonoBehaviour
{
    SpriteRenderer color;

    void Start()
    {
        color = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            color.color = Color.yellow;
        }
    }
}
