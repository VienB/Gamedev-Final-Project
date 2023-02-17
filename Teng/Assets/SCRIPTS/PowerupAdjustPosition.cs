using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAdjustPosition : MonoBehaviour
{
    // Float Variable
    public float raycastDistance = 20.0f;

    // Script Communication
    private PowerupSpawner spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // Finding Component reference
        spawnManager = GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<PowerupSpawner>();
        // Use Raycast to determine the position of the powerups
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 20.0f))
        {
            spawnManager.spawnPosition = hit.point;
            Destroy(gameObject);
        }
    }
}
