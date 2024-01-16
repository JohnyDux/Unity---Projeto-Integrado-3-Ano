using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollingPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI correctPiecesText;
    public RotateCylinder[] cylinders;

    private int propertyCounter = 0;

    void Update()
    {
        // Check and update the integer based on changes in the boolean property
        CheckAndUpdateCounter();

        // Do something based on the result
        correctPiecesText.text = "Correct Pieces: " + propertyCounter;
    }

    void CheckAndUpdateCounter()
    {
        foreach (RotateCylinder obj in cylinders)
        {
            if (obj != null)
            {
                bool previousValue = obj.correctPosition;
                // Check if the property has changed
                if (previousValue != obj.correctPosition)
                {
                    // Increment or decrement the counter based on the change
                    propertyCounter += obj.correctPosition ? 1 : -1;
                }
            }
        }
    }
}
