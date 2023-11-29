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
    public ParticleSystem explosionEffect;
    public ParticleSystem pulseEffect;
    public Rigidbody2D rb;
    public float speed = 500f;
    public bool isHit;
    public int scoreValue = 1;
    public LayerMask layerMask;

    void Start()
    {
        isHit = false;
        color.color = StartColor;
        FindObjectOfType<Score>().ScoreUpdate(0);
    }


    void Update()
    {
        rb.velocity = new Vector3(0, -speed*Time.deltaTime, 0);
    }
    void OnMouseOver()
    {
        var main = explosionEffect.main;

        if (Input.GetMouseButtonDown(0) && isHit == false)
        {
            int rand = Random.Range(0, colorsList.Count);
            color.color = colorsList[rand];
            explosionEffect.Play();
            pulseEffect.Play();
            main.startColor = colorsList[rand];

            isHit = true;
            FindObjectOfType<Score>().ScoreUpdate(scoreValue);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "border")
        {
            if (color.color != StartColor)
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
