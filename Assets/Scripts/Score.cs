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

    [Header("Insert here the necessary points to win the level")]
    public int pointsToWin;

    [Header("Time Left in Seconds")]
    [Range(0, 90)] public float timeLeft;
    public TextMeshProUGUI timerText;

    public Animator ViolinistAnimator;
    public ParticleSystem ViolinistSoundParticles;
    public Animator BateristAnimator;
    public ParticleSystem BateristSoundParticles;
    private void Start()
    {
        timeLeft -= Time.deltaTime;

        string minutesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
        string seconds = (timeLeft % 60).ToString("F0");
        seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

        timerText.text = minutesLeft + ":" + seconds;

        if (ViolinistSoundParticles == null)
        {
            ViolinistSoundParticles = GetComponent<ParticleSystem>();
        }
    }
    public int ScoreUpdate(int score)
    {
        lastScore = scorePoints;
        scorePoints = lastScore + score;
        scoreText.text = scorePoints.ToString("0") + " Hit";
        return 0;
    }

    public void Update()
    {
        if(scorePoints > lastScore)
        {
            ViolinistAnimator.SetBool("IsPlaying", true);
            BateristAnimator.SetBool("IsPlaying", true);
            StartParticleSystem(ViolinistSoundParticles);
            StartParticleSystem(BateristSoundParticles);
        }
        else
        {
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
            StopParticleSystem(ViolinistSoundParticles);
            StopParticleSystem(BateristSoundParticles);
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
            if(scorePoints > pointsToWin)
            {
                timerText.text = "You Won";
            }
            else
            {
                timerText.text = "You Lost";
            }
            Time.timeScale = 0f;
        }
    }

    void StartParticleSystem(ParticleSystem SoundParticles)
    {
        if (SoundParticles != null && !SoundParticles.isPlaying)
        {
            SoundParticles.Play();
        }
    }

    void StopParticleSystem(ParticleSystem SoundParticles)
    {
        if (SoundParticles != null && SoundParticles.isPlaying)
        {
            SoundParticles.Stop();
        }
    }
}
