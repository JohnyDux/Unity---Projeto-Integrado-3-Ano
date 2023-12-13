using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int scorePoints;
    public TextMeshProUGUI missText;
    public DestroyerCheckers check;

    [Header("Time Left in Seconds")]
    public float timeLeft = 300;
    
    public TextMeshProUGUI timerText;

    private void Start()
    {
        timeLeft -= Time.deltaTime;

        string minutesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
        string seconds = (timeLeft % 60).ToString("F0");
        seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

        timerText.text = minutesLeft + ":" + seconds;
    }
    public void ScoreUpdate(int score)
    {
        scorePoints += score;
        scoreText.text = scorePoints.ToString("0") + " Hit";

        missText.text = check.numberOfMisses.ToString("0") + " Missed";
    }

    public void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime * 2;

            string minutesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
            string seconds = (timeLeft % 60).ToString("F0");
            seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

            timerText.text = minutesLeft + ":" + seconds;
        }

        else
        {
            Time.timeScale = 0f;
        }
    }
}
