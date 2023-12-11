using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Time Left in Seconds")]
    public float timeLeft = 300;

    public TextMeshProUGUI timerText;

    public void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime * 2;

            string minituesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
            string seconds = (timeLeft % 60).ToString("F0");
            seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

            timerText.text = minituesLeft + ":" + seconds;
        }

        else
        {
            Time.timeScale = 0f;
        }
    }
}
