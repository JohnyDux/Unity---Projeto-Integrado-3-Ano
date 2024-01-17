using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollingPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI correctPiecesText;
    public RotateCylinder[] cylinders;

    public int correctPieces;

    private bool[] previousBooleanStates;

    void Start()
    {
        // Initialize the array to store previous boolean states
        previousBooleanStates = new bool[cylinders.Length];
        for (int i = 0; i < cylinders.Length; i++)
        {
            if (cylinders[i] != null)
            {
                previousBooleanStates[i] = cylinders[i].correctPosition;
            }
        }
    }

    void Update()
    {
        correctPiecesText.text = "Correct Pieces: " + correctPieces;

        // Check for changes in the boolean property and modify the variable accordingly
        for (int i = 0; i < cylinders.Length; i++)
        {
            if (cylinders[i] != null)
            {
                if (cylinders[i].correctPosition != previousBooleanStates[i])
                {
                    // Update the variable based on the change in the boolean property
                    correctPieces += cylinders[i].correctPosition ? 1 : -1;

                    // Update the previous state
                    previousBooleanStates[i] = cylinders[i].correctPosition;
                }
            }
        }
    }
}
