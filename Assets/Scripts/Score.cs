using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int scorePoints;
    int lastScore;
    public TextMeshProUGUI missText;
    public DestroyerCheckers check;

    [Header("Time Left in Seconds")]
    [Range(0, 90)] public float timeLeft;
    public TextMeshProUGUI timerText;

    public Animator ViolinistAnimator;
    public ParticleSystem SoundParticles;
    private void Start()
    {
        timeLeft -= Time.deltaTime;

        string minutesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
        string seconds = (timeLeft % 60).ToString("F0");
        seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

        timerText.text = minutesLeft + ":" + seconds;

        if (SoundParticles == null)
        {
            SoundParticles = GetComponent<ParticleSystem>();
        }
    }
    public int ScoreUpdate(int score)
    {
        lastScore = scorePoints;
        scorePoints = lastScore + score;
        scoreText.text = scorePoints.ToString("0") + " Hit";

        missText.text = check.numberOfMisses.ToString("0") + " Missed";
        return 0;
    }

    public void Update()
    {
        if(scorePoints > lastScore)
        {
            ViolinistAnimator.SetBool("IsPlaying", true);
            StartParticleSystem();
        }
        else
        {
            ViolinistAnimator.SetBool("IsPlaying", false);
            StopParticleSystem();
        }

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

    void StartParticleSystem()
    {
        if (SoundParticles != null && !SoundParticles.isPlaying)
        {
            SoundParticles.Play();
        }
    }

    void StopParticleSystem()
    {
        if (SoundParticles != null && SoundParticles.isPlaying)
        {
            SoundParticles.Stop();
        }
    }
}
