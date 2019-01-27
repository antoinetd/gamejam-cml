using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_ScoreDisplay : MonoBehaviour
{
    public float GameScore; // Debug output value
    
    public float GameRoundTime;
    private TextMeshPro TextMeshObject;
    private GameObject GameManagerScript;
    private float currentTimer; // 

    // Start is called before the first frame update
    void Start()
    {
        TextMeshObject = GetComponent<TextMeshPro>();

        currentTimer = GameRoundTime;


    }

    // Update is called once per frame
    void Update()
    {
        GameScore = GameManager_Scoring.GetInstance().GetGameScore();

        
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0.0)
        {
            currentTimer = 0.0f;
        }

        TextMeshObject.text = "Nostalgia Points: " + string.Format("{0:N0}", GameScore) + "\nCountdown: " + string.Format("{0:N0}", currentTimer);
    }
}
