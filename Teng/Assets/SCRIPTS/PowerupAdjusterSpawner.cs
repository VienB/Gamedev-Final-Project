using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAdjusterSpawner : MonoBehaviour
{
    // GameObject Variables
    public GameObject powerupAdjuster;
    public GameObject player;

    // Float Variable
    public float zPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        // Spawning the object needed for raycast
        StartCoroutine(SpawnAdjusters());
    }

    // Update is called once per frame
    void Update()
    {
        // Finding gameobject
        player = GameObject.FindGameObjectWithTag("Player");
        // If player is not null then the transform of the object equal the player transform
        if (player != null)
        {
            transform.position = player.transform.position;
        }
        // If the gameobject is not null set the zPosition to the transform position plus 100;
        if(gameObject != null)
        {
            zPosition = transform.position.z + 100.0f;
        }
    }
    // Spawning the object for raycast at a given interval
    public IEnumerator SpawnAdjusters()
    {
        yield return new WaitForSeconds(1);
        float xPosition = Random.Range(-3.0f, 3.0f);
        Instantiate(powerupAdjuster, new Vector3(xPosition, 15.0f, zPosition), powerupAdjuster.transform.rotation);
        StartCoroutine(SpawnAdjusters());
    }
}
