using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TileAction : MonoBehaviour
{
    //Color of the tile
    public GameObject ColorOverlay;

    //Physics and Velocity
    public Rigidbody2D rb;
    [HideInInspector]public float fallingSpeed;

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
        ColorOverlay.SetActive(false);
        scoreRef = FindObjectOfType<Score>();

        if (SoundParticles == null)
        {
            SoundParticles = GetComponent<ParticleSystem>();
        }

        fallingSpeed = Random.Range(7f, 15f);

        if (scoreRef.timeLeft > 60 && scoreRef.timeLeft < 90)
        {
            rb.velocity = new Vector3(0, -fallingSpeed * Time.deltaTime, 0);
        }

        else if (scoreRef.timeLeft > 30 && scoreRef.timeLeft < 60)
        {
            rb.velocity = new Vector3(0, -fallingSpeed * 1.5f * Time.deltaTime, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, -fallingSpeed * 2.2f * Time.deltaTime, 0);
        }
    }

    void OnMouseOver()
    {
        if(Time.timeScale == 1f)
        {
            if (Input.GetMouseButtonDown(0) && isHit == false)
            {
                ColorOverlay.SetActive(true);
                StartParticleSystem();
                scoreRef.ScoreUpdate(1);
                isHit = true;

                fallingSpeed = 1000f;
            }
        }
        else
        {
            fallingSpeed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "border")
        {
            if (ColorOverlay != null && ColorOverlay.activeSelf)
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
