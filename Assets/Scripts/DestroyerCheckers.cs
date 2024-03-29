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

    public GameObject LostScreen;
    public GameObject menuButton;

    public SpawnerAction spawnerAction;
    private void Start()
    {
        numberOfMisses = 0;
        menuButton.SetActive(false);
        LostScreen.SetActive(false);
    }

    void Update()
    {
        if (numberOfMisses > maxMisses)
        {
            Time.timeScale = 0f;
            menuButton.SetActive(true);
            LostScreen.SetActive(true);
        }

        missText.text = numberOfMisses.ToString("0") + " Missed";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TileAction tile = collision.GetComponent<TileAction>();

        if (collision.CompareTag("piano"))
        {
            spawnerAction.currentAmountPieces--;
        }

        if (!tile.isHit && collision.CompareTag("piano"))
        {
            numberOfMisses++;
            Destroy(collision.gameObject);
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
