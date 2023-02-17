using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollision : MonoBehaviour
{
    // Float Variable
    private float rotateCoins = 2.0f;

    // GameObject Variable
    public GameObject coins;

    // Setting the coin sound volume at the start
    void Start()
    {
        PlayerController.instance.coinSoundSource.volume = 0.3f;
    }
    // Update is called once per frame
    void Update()
    {
        // Rotating the coins
        transform.Rotate(0, 0, rotateCoins);   
    }
    void OnTriggerEnter(Collider other)
    {
        // If coin collides with player, play coinsound, add coin value, destroy coin
        if (other.tag == "Player")
        {
            PlayerController.instance.coinSoundSource.PlayOneShot(PlayerController.instance.coinSound);
            CoinBoard.coinCollection += 1;
            Destroy(gameObject);

        }
    }
}
