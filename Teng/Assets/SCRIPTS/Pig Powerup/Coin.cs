using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Transform Variable
    public Transform playerTransform;
    
    // Float Variable
    public float moveSpeed = 17.0f;
    
    // Script Communication
    CoinMove coinMove;
    // Start is called before the first frame update
    void Start()
    {
        // Getting game object references
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Getting component references
        coinMove = gameObject.GetComponent<CoinMove>();
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        // If the coin enters the collider of coin detector, enable the coinmove component
        if(other.gameObject.tag == "CoinDetector")
        {
            coinMove.enabled = true;
        }
    }
}
