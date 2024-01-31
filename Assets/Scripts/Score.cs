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

    [Header("Win/Lost Screen Sprites")]
    public GameObject WinScreen;
    public GameObject LostScreen;
    public GameObject WinConfetti;

    [Header("Time Left in Seconds")]
    [Range(0, 90)] public float timeLeft;
    public TextMeshProUGUI timerText;

    [Header("Characters Animation and Particles")]
    public Animator ViolinistAnimator;
    public ParticleSystem ViolinistSoundParticles;
    public Animator BateristAnimator;
    public ParticleSystem BateristSoundParticles;
    public Animator PianistAnimator;
    public ParticleSystem PianistSoundParticles;
    public Animator SaxophonistAnimator;
    public ParticleSystem SaxophonistSoundParticles;

    public TileAction pianoTile;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public string mixerGroupName;
    public float volumeChangeSpeed = 1.0f;
    float targetVolume;
    private AudioMixerGroup audioMixerGroup;
    public AudioSource audioSource;

    public AudioClip pianoMusic;
    public AudioClip lostSound;
    public AudioClip winSound;

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
            audioSource.clip = pianoMusic;
        }

        targetVolume = -80.0f;

        SetVolume(targetVolume);

        pianoTile.fallingSpeed = 100f;

        WinScreen.SetActive(false);
        LostScreen.SetActive(false);
        WinConfetti.SetActive(false);
    }
    public int ScoreUpdate(int score)
    {
        if(timeLeft > 0)
        {
            lastScore = scorePoints;
            scorePoints = lastScore + score;
            scoreText.text = scorePoints.ToString("0") + " Hit";
            return 0;
        }
        else
        {
            return 0;
        }
    }

    public void Update()
    {
        if (scorePoints < 2)
        {
            targetVolume = 2.0f;
            SetVolume(targetVolume);
        }

        if (scorePoints/1.5 > checkers.numberOfMisses && scorePoints >= 2 && scorePoints < pointsToWin)
        {
            ViolinistAnimator.SetBool("IsPlaying", true);
            BateristAnimator.SetBool("IsPlaying", true);
            PianistAnimator.SetBool("IsPlaying", true);
            SaxophonistAnimator.SetBool("IsPlaying", true);

            StartParticleSystem(ViolinistSoundParticles);
            StartParticleSystem(BateristSoundParticles);
            StartParticleSystem(PianistSoundParticles);
            StartParticleSystem(SaxophonistSoundParticles);

            targetVolume = 2.0f;
        }
        else
        {
            ViolinistAnimator.SetBool("IsPlaying", false);
            BateristAnimator.SetBool("IsPlaying", false);
            PianistAnimator.SetBool("IsPlaying", false);
            SaxophonistAnimator.SetBool("IsPlaying", false);

            StopParticleSystem(ViolinistSoundParticles);
            StopParticleSystem(BateristSoundParticles);
            StopParticleSystem(PianistSoundParticles);
            StopParticleSystem(SaxophonistSoundParticles);

            targetVolume = -80.0f;
        }

        if(scorePoints >= 2 && scorePoints < pointsToWin)
        {
            ChangeVolumeOverTime(targetVolume);
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
            if (scorePoints > pointsToWin)
            {
                audioSource.clip = winSound;

                WinScreen.SetActive(true);
                WinConfetti.SetActive(true);
                LostScreen.SetActive(false);
                timerText.text = "";

                ViolinistAnimator.SetBool("IsPlaying", false);
                BateristAnimator.SetBool("IsPlaying", false);
                PianistAnimator.SetBool("IsPlaying", false);
                SaxophonistAnimator.SetBool("IsPlaying", false);

                StopParticleSystem(ViolinistSoundParticles);
                StopParticleSystem(BateristSoundParticles);
                StopParticleSystem(PianistSoundParticles);
                StopParticleSystem(SaxophonistSoundParticles);

                pianoTile.fallingSpeed = 0f;
            }
            else
            {
                audioSource.clip = lostSound;

                WinScreen.SetActive(false);
                WinConfetti.SetActive(false);
                LostScreen.SetActive(true);
                timerText.text = "";

                ViolinistAnimator.SetBool("IsPlaying", false);
                BateristAnimator.SetBool("IsPlaying", false);
                PianistAnimator.SetBool("IsPlaying", false);
                SaxophonistAnimator.SetBool("IsPlaying", false);

                StopParticleSystem(ViolinistSoundParticles);
                StopParticleSystem(BateristSoundParticles);
                StopParticleSystem(PianistSoundParticles);
                StopParticleSystem(SaxophonistSoundParticles);

                pianoTile.fallingSpeed = 0f;
            }
            audioMixer.SetFloat("SoundEffects", -80);
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
