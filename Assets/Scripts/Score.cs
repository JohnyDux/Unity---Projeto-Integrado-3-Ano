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
    public void ScoreUpdate(int score)
    {
        scorePoints += score;
        scoreText.text = scorePoints.ToString("0") + " Hit";

        missText.text = check.numberOfMisses.ToString("0") + " Missed";
    }
}
