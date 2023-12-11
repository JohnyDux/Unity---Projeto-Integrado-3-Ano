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

    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
        }
        else
        {
            timeText.text = "You Lost";
            Time.timeScale = 0f;
        }
    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
