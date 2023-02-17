using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Animator Variable
    private Animator npcAnim;

    //Float Variables
    public float timer;
    public float yPos = 1.0f;
    public float zPos = -2.5f;
    public float zPosNearPlayer = -1.2f;
    public float speedFollow = 5f;
    private float y;

    // Vector3 Variables
    public Vector3 comparisonOffset;
    public Vector3 nearPlayer;
    public Vector3 offset;

    // Transform Variable
    public Transform target;
    
    void Start()
    {
        // Getting component reference
        npcAnim = GameObject.Find("Enemy").GetComponent<Animator>();

        // Setting necessary values
        timer = 5.0f;
        nearPlayer = new Vector3(0, yPos, zPosNearPlayer);
        comparisonOffset = new Vector3(0, yPos, zPos);
        offset = comparisonOffset;
    }

    void LateUpdate()
    {
        // Making the enemy follow the player
        Vector3 newPosition = target.position + offset;
        RaycastHit hit;
        if (Physics.Raycast(target.position, Vector3.down, out hit, 2.5f))
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * speedFollow);
        else y = Mathf.Lerp(y, target.position.y, Time.deltaTime * speedFollow);
        newPosition.y = offset.y + y;
        transform.position = newPosition;

        // If the enemy is near, countdown
        while (offset != comparisonOffset)
        {
            timer -= 1 * Time.deltaTime;
            Debug.Log(timer);

            // If timer is less than 0.3f then play animation
            if (timer < 0.3f)
            {
                npcAnim.Play("Rolling");
            }
            // if timer is less than 0.0f then set offset, and timer value
            if (timer < 0)
            {
                offset = comparisonOffset;
                timer = 5.0f;
            }
            break;
        }
    }
}
