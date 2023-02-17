using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // GameObject Variables
    public GameObject[] spawningobstaclePrefab;
    private GameObject player;

    // Script communication
    private PlayerController playerController;
    private Buttons buttons;

    // Float Variables
    public float spawnDelay = 5.0f;
    private float manilaSpawnStart = 90.0f;
    private float sanJuanicoSpawnStart = 140.0f;
    private float viganSpawnStart = 70.0f;
    private float manilaSpawn = 165.5f;
    private float sanJuanicoSpawn = 210.0f;
    private float viganSpawn = 141.95f;

    // Getting necessary components, and initial spawning
    public void GettingComponents()
    {
        player = GameObject.Find("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        buttons = GameObject.Find("BUTTONS").GetComponent<Buttons>();
        int obstacleIndex = Random.Range(0, spawningobstaclePrefab.Length);
        // If obstacle index is equal to 0 then run line of code
        if (obstacleIndex == 0)
        {
            Vector3 spawnPos = new Vector3(-14.1f, 19.25f, (player.transform.position.z + manilaSpawnStart));
            Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
        }
        // If obstacle index is equal to 1 then run line of code
        if (obstacleIndex == 1)
        {
            Vector3 spawnPos = new Vector3(0.0f, -0.66f, (player.transform.position.z + sanJuanicoSpawnStart));
            Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
        }
        // If obstacle index is equal to 2 then run line of code
        if (obstacleIndex == 2)
        {
            Vector3 spawnPos = new Vector3(0.0f, -0.5f, (player.transform.position.z + viganSpawnStart));
            Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
        }
    }
    // Method for spawning the terrain
    public void spawningObstacle()
    {
        if (playerController.gameOver == false && buttons.gamePause == false)
        {
            int obstacleIndex = Random.Range(0, spawningobstaclePrefab.Length);
            if(obstacleIndex == 0)
            {
                Vector3 spawnPos = new Vector3(-14.1f, 19.25f, (player.transform.position.z + manilaSpawn));
                Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
            }
            if(obstacleIndex == 1)
            {
                Vector3 spawnPos = new Vector3(0.0f, -0.66f, (player.transform.position.z + sanJuanicoSpawn));
                Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
            }
            if (obstacleIndex == 2)
            {
                Vector3 spawnPos = new Vector3(0.0f, -0.5f, (player.transform.position.z + viganSpawn));
                Instantiate(spawningobstaclePrefab[obstacleIndex], spawnPos, spawningobstaclePrefab[obstacleIndex].transform.rotation);
            }
        }
    }
}
