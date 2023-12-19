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
    public Rigidbody2D rb;
    public float fallingSpeed = 500f;
    public bool isHit;
    public int scoreValue = 1;
    public LayerMask layerMask;

    public ParticleSystem SoundParticles;
    Score scoreRef;
    void Start()
    {
        isHit = false;
        color.color = StartColor;
        scoreRef = FindObjectOfType<Score>();

        if (SoundParticles == null)
        {
            SoundParticles = GetComponent<ParticleSystem>();
        }
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
