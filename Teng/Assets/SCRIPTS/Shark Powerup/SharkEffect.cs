using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEffect : MonoBehaviour
{
    // BoxCollider Variable
    private BoxCollider obstacleCollider;

    // Script Communication
    private OnCollision onCollisionScript;

    private void Start()
    {
        // Getting component references
        obstacleCollider = GetComponent<BoxCollider>();
        onCollisionScript = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<OnCollision>();
    }

    private void Update()
    {
        // If the shark powerup boolean is true, then enable the istrigger of collider
        if (onCollisionScript.sharkPowerUp == true)
        {
            obstacleCollider.isTrigger = true;
        }
        // If the shark powerup boolean is false, then disable the istrigger of collider
        else if (onCollisionScript.sharkPowerUp == false)
        {
            obstacleCollider.isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the object collides with the "Player" then destroy the object
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
