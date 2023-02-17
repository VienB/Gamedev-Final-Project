using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainDeleter : MonoBehaviour
{
    // GameObject Variable
    private GameObject player;

    // Float Variable
    private float terrainLimit;
    private void Start()
    {
        // Getting object references
        player = GameObject.FindGameObjectWithTag("Player");

        // Setting the terrain limit to 0.0f
        terrainLimit = 0.0f;
    }
    private void Update()
    {
        // To fix NullReferenceException, if player is not null, then set terrain limit equal to the position of the player
        if (player != null)
        {
            terrainLimit = player.transform.position.z;
        }
        // If the game object is less than the terrain limit minus 150.0f, call the TerrainDelete method
        if(transform.position.z < terrainLimit - 150.0f)
        {
            TerrainDelete();
        }
    }
    // Deleting the terrain method
    public void TerrainDelete()
    {
        Destroy(gameObject);
    }
}
