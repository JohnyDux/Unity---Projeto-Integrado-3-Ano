using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the GameManager instance
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            // Get a random sprite from the GameManager
            Sprite randomSprite = gameManager.GetRandomSprite();

            if (randomSprite != null)
            {
                // Set the sprite on the SpriteRenderer
                spriteRenderer.sprite = randomSprite;
            }
            else
            {
                Debug.LogError("Failed to get a random sprite from the GameManager.");
            }
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
}
