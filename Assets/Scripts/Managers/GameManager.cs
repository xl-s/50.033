using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    public int playerScore = 0;

    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;

    public void increaseScore()
    {
        this.playerScore++;
        this.score.text = $"SCORE: {this.playerScore.ToString().PadLeft(4, '0')}";
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }
}
