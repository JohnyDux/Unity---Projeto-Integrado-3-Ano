using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Score System")]
    public TextMeshProUGUI scoreText;
    public int scorePoints;
    public int lastScore;

    [Header("Insert here the necessary points to win the level")]
    public int pointsToWin;
    public DestroyerCheckers checkers;

    [Header("Time Left in Seconds")]
    [Range(0, 90)] public float timeLeft;
    public TextMeshProUGUI timerText;

    [Header("Animation and Particles")]
    public Animator ViolinistAnimator;
    public ParticleSystem ViolinistSoundParticles;
    public Animator BateristAnimator;
    public ParticleSystem BateristSoundParticles;
    public Animator PianistAnimator;
    public ParticleSystem PianistSoundParticles;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public string mixerGroupName;
    public float volumeChangeSpeed = 1.0f;
    public float targetVolume = 20.0f;
    private AudioMixerGroup audioMixerGroup;
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

        if (audioMixer != null)
        {
            // Get the Audio Mixer Group by name
            audioMixerGroup = audioMixer.FindMatchingGroups(mixerGroupName)[0];
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
        if(scorePoints > checkers.numberOfMisses)
        {
            ViolinistAnimator.SetBool("IsPlaying", true);
            BateristAnimator.SetBool("IsPlaying", true);
            PianistAnimator.SetBool("IsPlaying", true);

            StartParticleSystem(ViolinistSoundParticles);
            StartParticleSystem(BateristSoundParticles);
            StartParticleSystem(PianistSoundParticles);

            targetVolume = 20.0f;
        }
        else
        {
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
            PianistAnimator.SetBool("IsPlaying", false);

            StopParticleSystem(ViolinistSoundParticles);
            StopParticleSystem(BateristSoundParticles);
            StopParticleSystem(PianistSoundParticles);

            targetVolume = -80.0f;
        }
        ChangeVolumeOverTime(targetVolume);

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
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
            PianistAnimator.SetBool("IsPlaying", false);

            StopParticleSystem(ViolinistSoundParticles);
            StopParticleSystem(BateristSoundParticles);
            StopParticleSystem(PianistSoundParticles);

            audioMixer.SetFloat("SoundEffects", -80);
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

    // Function to change volume over time
    void ChangeVolumeOverTime(float targetVolume)
    {
        float currentVolume;
        audioMixer.GetFloat(mixerGroupName, out currentVolume);

        // Interpolate towards the target volume
        float newVolume = Mathf.Lerp(currentVolume, targetVolume, Time.deltaTime * volumeChangeSpeed);

        // Set the new volume to the Audio Mixer Group
        audioMixer.SetFloat(mixerGroupName, newVolume);
    }

    // Function to set the volume instantly
    public void SetVolume(float newVolume)
    {
        // Set the new volume to the Audio Mixer Group
        audioMixer.SetFloat(mixerGroupName, newVolume);
    }
}
