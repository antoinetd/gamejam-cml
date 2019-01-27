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

    public GameObject HPBar;

    // Private Variables
    private SpriteRenderer HPBarSpriteRenderer;
    private float HPBarSpriteInitialWidth;
    private float HPBarSpriteInitialHeight;


// Start is called before the first frame update
void Start()
    {

        TextMeshObject = GetComponent<TextMeshPro>();

        currentTimer = GameRoundTime;

        // Prepare the health bar
        HPBarSpriteRenderer = HPBar.GetComponent<SpriteRenderer>();
        HPBarSpriteRenderer.color = Color.green;

        HPBarSpriteInitialWidth = HPBarSpriteRenderer.size.x;
        HPBarSpriteInitialHeight = HPBarSpriteRenderer.size.y;


}

    // Update is called once per frame
    void Update()
    {
        GameScore = GameManager_Scoring.GetInstance().GetGameScore();

        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            currentTimer = 0f;
        }
            

        // Update the sprite size
        HPBarSpriteRenderer.size = new Vector2(HPBarSpriteInitialWidth * currentTimer / GameRoundTime, HPBarSpriteInitialHeight);

        TextMeshObject.text = "Nostalgia Points: " + string.Format("{0:N0}", GameScore); //+ "\nCountdown: " + string.Format("{0:N0}", currentTimer)
    }
}
