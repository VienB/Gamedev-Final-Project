using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinBoard : MonoBehaviour
{
    // Script Communication
    private PlayerController playerController;

    // Int Variable
    public static int coinCollection = 0;

    // To use TextMeshProUGUI
    TextMeshProUGUI coin;
    // Start is called before the first frame update
    void Start()
    {
        // Getting component references
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        coin = gameObject.GetComponent<TextMeshProUGUI>();
      
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is not over update coin value in UI
        if(playerController.gameOver == false)
        {
            coin.text = "" + coinCollection;
        }
        // If the game is over update coin value in UI
        else if (playerController.gameOver == true)
        {
            coin.text = "" + coinCollection;
        }
    }
    // Resetting the coin value
    public void ResettingCoin()
    {
        coinCollection = 0;
    }
}
