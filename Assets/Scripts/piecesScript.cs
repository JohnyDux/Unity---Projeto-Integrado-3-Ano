﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piecesScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    transform.position = RightPosition;
                    InRightPosition = true;                    
                    GetComponent<SortingGroup>().sortingOrder = 0;                    
                }
            }
        }
    }
}
