using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TileAction : MonoBehaviour
{
    //Color of the tile
    public SpriteRenderer color;
    public Color StartColor;
    public List<Color> colorsList;
    Color ClickColor;

    //Physics and Velocity
    public Rigidbody2D rb;
    public float fallingSpeed;

    //Trigger and Score
    public bool isHit;
    Score scoreRef;
    public int scoreValue = 1;
    public LayerMask layerMask;

    //Particles
    public ParticleSystem SoundParticles;

    void Start()
    {
        isHit = false;
        color.color = StartColor;
        scoreRef = FindObjectOfType<Score>();

        if (SoundParticles == null)
        {
            SoundParticles = GetComponent<ParticleSystem>();
        }

        fallingSpeed = Random.Range(1000f, 2000f);

        rb.velocity = new Vector3(0, -fallingSpeed * Time.deltaTime, 0);
    }

    void Update()
    {
        rb.velocity = new Vector3(0, -fallingSpeed * Time.deltaTime, 0);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isHit == false)
        {
            int rand = Random.Range(0, colorsList.Count);
            color.color = colorsList[rand];
            StartParticleSystem();
            scoreRef.ScoreUpdate(1);
            isHit = true;

            fallingSpeed = 1000f;
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

    void StartParticleSystem()
    {
        if (SoundParticles != null && !SoundParticles.isPlaying)
        {
            SoundParticles.Play();
        }
    }
}
