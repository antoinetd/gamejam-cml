using System;
using UnityEngine;

public class GameManager_Scoring
{
    static GameManager_Scoring instance;

    // GameManager_Scoring should be a singleton
    public static GameManager_Scoring GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager_Scoring();
        }
        return instance;
    }

    public float GameScore;

    public bool isAdult = false;
    public void AddToScore(float points)
    {
        if (isAdult)
        {
            GameScore = GameScore - points;
        }
        else
        {
            GameScore = GameScore + points;
        }
    }

    public float GetGameScore()
    {
        return GameScore;

    }
}
