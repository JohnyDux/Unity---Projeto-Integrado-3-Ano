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

    public MazePlayerController player;
    public TextMeshProUGUI lifesText;

    void Update()
    {
        if(timeLeft > 0 && player.lifes > 0)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
        }

        if (timeLeft > 0 && score == 5)
        {
            timeText.text = "You Won";
            Time.timeScale = 0f;
        }

        if (player.lifes == 0)
        {
            timeText.text = "You Lost";
            Time.timeScale = 0f;
        }

        if (timeLeft == 0 && score < 5)
        {
            timeText.text = "You Lost";
            Time.timeScale = 0f;
        }
    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score.ToString();

        lifesText.text = "Lifes: " + player.lifes.ToString();
    }
}
