using UnityEngine;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{
    public int puzzleRows = 3;
    public int puzzleCols = 3;
    public float tileSize = 1.0f;

    public List<Transform> puzzleTiles = new List<Transform>();

    void Start()
    {
        CollectPuzzleTiles();
        ShufflePuzzle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Tile"))
            {
                MoveTile(hit.collider.transform);
            }
        }
    }

    void CollectPuzzleTiles()
    {
        // Collect all child transforms of the Puzzle GameObject
        foreach (Transform child in transform)
        {
            puzzleTiles.Add(child);
        }
    }

    void ShufflePuzzle()
    {
        int tileCount = puzzleTiles.Count;

        // Iterate through the puzzle tiles and swap their positions randomly
        for (int i = 0; i < tileCount; i++)
        {
            int randomIndex = Random.Range(i, tileCount);

            // Swap positions
            Vector3 tempPosition = puzzleTiles[i].position;
            puzzleTiles[i].position = puzzleTiles[randomIndex].position;
            puzzleTiles[randomIndex].position = tempPosition;
        }
    }

    void MoveTile(Transform tileTransform)
    {
        // Get the index of the clicked tile
        int clickedTileIndex = puzzleTiles.IndexOf(tileTransform);

        // Find the empty spot
        Transform emptySpot = puzzleTiles.Find(tile => tile.CompareTag("EmptySpot"));

        if (emptySpot != null)
        {
            // Swap positions of the clicked tile and the empty spot
            Vector3 tempPosition = tileTransform.position;
            tileTransform.position = emptySpot.position;
            emptySpot.position = tempPosition;

            // Update the puzzleTiles list to reflect the new arrangement
            puzzleTiles[clickedTileIndex] = tileTransform;
            puzzleTiles[puzzleTiles.IndexOf(emptySpot)] = emptySpot;
        }
    }

    bool IsAdjacent(Transform tileTransform, Transform emptySpot)
    {
        // Check if the tile is adjacent to the empty spot
        float distance = Vector3.Distance(tileTransform.position, emptySpot.position);
        return Mathf.Approximately(distance, tileSize);
    }


    void OnDrawGizmos()
    {
        // Draw a visual representation of the tile size using Gizmos
        Gizmos.color = Color.green;

        for (int i = 0; i < puzzleTiles.Count; i++)
        {
            Vector3 position = new Vector3(puzzleTiles[i].position.x, puzzleTiles[i].position.y, transform.position.z);
            Gizmos.DrawWireCube(position, new Vector3(tileSize, tileSize, 0.1f));
        }
    }
}
