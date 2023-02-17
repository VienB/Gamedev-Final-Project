using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    // Script communication
    Coin coin;
    // Start is called before the first frame update
    void Start()
    {
        // Getting component reference
        coin = gameObject.GetComponent<Coin>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving the coin towards the player's position
        transform.position = Vector3.MoveTowards(transform.position, coin.playerTransform.position, coin.moveSpeed * Time.deltaTime);
    }
}
