using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_ScoreDisplay : MonoBehaviour
{
    public float GameScore; // 
    private TextMeshPro TextMeshObject;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshObject = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        GameScore = GameManager_Scoring.GetInstance().GetGameScore();



        TextMeshObject.text = "Nostalgia Points: " + string.Format("{0:N0}", GameScore);
    }
}
