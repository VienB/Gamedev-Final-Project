using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transform Variable
    public Transform target;

    // Vector3 Variable
    private Vector3 offset;

    // Float Variables
    private float y;
    public float speedFollow = 5f;


    void Start()
    {
        // Setting the offset value
        offset = transform.position;
    }

    void LateUpdate()
    {
        // Make the camera follow the movement of the player
        Vector3 newPosition = target.position + offset;
        RaycastHit hit;
        if (Physics.Raycast(target.position, Vector3.down, out hit, 2.5f))
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * speedFollow);
        else y = Mathf.Lerp(y, target.position.y, Time.deltaTime * speedFollow);
        newPosition.y = offset.y + y;
        transform.position = newPosition;

    }
}