using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Script communication
    public Buttons buttons;
    private Audios audios;
    private PlayerController playerController;

    // Int Variables
    public static int scoreValue = 0;
    private int scoreBoundary;
    private int soundBoundary;
    
    // To use TextMeshProUGUI
    TextMeshProUGUI score;
    
    // Start is called before the first frame update
    void Start()
    {
        // Setting the value for scoreBoundary
        scoreBoundary = 500;
        soundBoundary = 2000;
        // Finding component references
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        score = GetComponent<TextMeshProUGUI>();
        buttons = GameObject.Find("BUTTONS").GetComponent<Buttons>();
        audios = GameObject.FindGameObjectWithTag("Audios").GetComponent<Audios>();
        

    }

    // Update is called once per frame
    void Update()
    {
        // If gameover and gamepause is false then run line of code
        if(playerController.gameOver == false && buttons.gamePause == false)
        {
            scoreValue += 1;
            score.text = "" + scoreValue;
            if(scoreValue == scoreBoundary)
            {
                playerController.forwardSpeed += 0.5f;
                scoreBoundary += 500;
            }
            if (scoreValue == soundBoundary)
            {
                audios.RandomSound();
                soundBoundary += 2000;
            }
        }
        else if(playerController.gameOver == true)
        {
            score.text = "" + scoreValue;
        }
    }
    // Method to reset the score
    public void ResettingScore()
    {
        scoreValue = 0;
    }
}
