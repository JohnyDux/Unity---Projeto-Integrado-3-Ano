using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTileScript : MonoBehaviour
{
    public Vector3 registeredPosition;
    public GameScript gameScript;

    void Start()
    {
        // Find the GameScript in the scene
        gameScript = FindObjectOfType<GameScript>();
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, registeredPosition));

        // Check if the tile is in its registered position
        if (Vector3.Distance(transform.position, registeredPosition) < 1.5f)
        {
            // Increase the score in the GameScript
            gameScript.score++;
            Debug.Log("Tile in position. New score: " + gameScript.score);
        }
    }
}
