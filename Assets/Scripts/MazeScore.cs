using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MazeScore : MonoBehaviour
{
    [Range(0, 30)] public float timeLeft;
    public TextMeshProUGUI timeText;

    public float score;
    public TextMeshProUGUI scoreText;

    public GameObject LostScreen;
    public GameObject WinScreen;
    public GameObject Confetis;

    public MazePlayerController player;
    public MazeAIChaser aIChaser;
    public TextMeshProUGUI lifesText;

    public GameObject MenuButton;
    public GameObject MusicSheetButton;

    private void Start()
    {
        MenuButton.SetActive(false);
        MusicSheetButton.SetActive(false);
        WinScreen.SetActive(false);
        LostScreen.SetActive(false);
        Confetis.SetActive(false);
    }

    void Update()
    {
        if(timeLeft > 0 && player.lifes > 0)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
        }

        if (timeLeft > 0 && score == 5)
        {
            timeText.text = "";
            WinScreen.SetActive(true);
            Confetis.SetActive(true);
            MenuButton.SetActive(true);
            MusicSheetButton.SetActive(true);

            player.moveSpeed = 0f;
            aIChaser.navMeshAgent.speed = 0f;
        }

        if (player.lifes == 0)
        {
            timeText.text = "";
            LostScreen.SetActive(true);
            MenuButton.SetActive(true);

            player.moveSpeed = 0f;
            aIChaser.navMeshAgent.speed = 0f;
        }

        if (timeLeft <= 0 && score < 5)
        {
            timeText.text = "";
            LostScreen.SetActive(true);
            MenuButton.SetActive(true);

            player.moveSpeed = 0f;
            aIChaser.navMeshAgent.speed = 0f;
        }
    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score.ToString();

        lifesText.text = "Lifes: " + player.lifes.ToString();
    }
}
