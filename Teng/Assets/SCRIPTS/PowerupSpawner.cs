using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    // GameObject Variable
    public GameObject[] powerupPrefabs;
    private GameObject player;

    // Script communication
    private PlayerController playerController;

    // Float variables
    public float sharkXPos = 0.5f;
    public float sharkYpos = 0.25f;
    public float pigXPos = 0.2f;
    public float pigYPos = 0.05f;
    public float kiteXPos = -0.2f;
    public float kiteYPos = 0.0f;
    private float spawnDelay = 1.0f;
    private float spawnRate = 20.0f;

    // Vector3 Variable
    public Vector3 spawnPosition;

    void Start()
    {
        InvokeRepeating("SpawningPowerups", spawnDelay, spawnRate);
    }
    // Getting necessary components, and initial spawning
    public void GettingComponents()
    {
        // Finding component reference
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Finding gameobject reference
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SpawningPowerups()
    {
        // Setting value for Powerup Index
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);

        // Choosing what powerup to spawn
        if (powerupIndex == 0)
        {
            Instantiate(powerupPrefabs[powerupIndex], spawnPosition, powerupPrefabs[powerupIndex].transform.rotation);
        }
        if (powerupIndex == 1)
        {
            Instantiate(powerupPrefabs[powerupIndex], spawnPosition, powerupPrefabs[powerupIndex].transform.rotation);
        }
        if (powerupIndex == 2)
        {
            Instantiate(powerupPrefabs[powerupIndex], spawnPosition, powerupPrefabs[powerupIndex].transform.rotation);
        }
    }
}
