using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(-0.32f, 5.21f), Random.Range(-4.95f, 5.06f));
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (Selected)
            {
                transform.position = RightPosition;
                InRightPosition = true;
            }
        }
    }
}
