using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileAction : MonoBehaviour
{
    public SpriteRenderer color;
    public Color StartColor;
    public List<Color> colorsList;
    Color ClickColor;
    public bool isHit;
    public int scoreValue = 1;

    void Start()
    {
        isHit = false;
        color.color = StartColor;
        FindObjectOfType<Score>().ScoreUpdate(0);
    }


    void Update()
    {
        
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int rand = Random.Range(0, colorsList.Count);
            color.color = colorsList[rand];

            isHit = true;
            FindObjectOfType<Score>().ScoreUpdate(scoreValue);
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
                SceneManager.UnloadSceneAsync("Game");
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
