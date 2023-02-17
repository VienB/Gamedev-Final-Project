using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    // Script communication
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If object collides with player, spawn a new terrain
        if(other.CompareTag("Player"))
        {
            spawnManager.spawningObstacle();
        }
    }
}
