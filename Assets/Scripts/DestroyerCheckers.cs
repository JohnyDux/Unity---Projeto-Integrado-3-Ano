using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DestroyerCheckers : MonoBehaviour
{
    public int numberOfMisses;
    public int maxMisses = 20;
    public Animator ViolinistAnimator;
    public Animator BateristAnimator;

    public TextMeshProUGUI missText;

    public GameObject menuButton;
    private void Start()
    {
        numberOfMisses = 0;
        menuButton.SetActive(false);
    }

    void Update()
    {
        if (numberOfMisses > maxMisses)
        {
            Time.timeScale = 0f;
            menuButton.SetActive(true);
        }

        missText.text = numberOfMisses.ToString("0") + " Missed";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TileAction tile = collision.GetComponent<TileAction>();
        if (!tile.isHit && collision.CompareTag("piano"))
        {
            numberOfMisses++;
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
        } 
        Destroy(collision.gameObject);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
