using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerCheckers : MonoBehaviour
{
    public int numberOfMisses;
    public Animator ViolinistAnimator;

    public GameObject menuButton;
    private void Start()
    {
        numberOfMisses = 0;
        menuButton.SetActive(false);
    }

    void Update()
    {
        if (numberOfMisses > 5)
        {
            Time.timeScale = 0f;
            menuButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TileAction tile = collision.GetComponent<TileAction>();
        if (!tile.isHit)
        {
            numberOfMisses++;
            ViolinistAnimator.SetBool("IsPlaying", false);
        } 
        Destroy(collision.gameObject);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
