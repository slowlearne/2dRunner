using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform target;                           // Reference to the player's transform
    public Vector3 offset;                            // Offset between the camera and the player.This will keep the camera a certain distance away from the player.

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Calculate the new position of the camera based on the player's current position and the offset.
        // The camera only follows the player on the x-axis (horizontal), while keeping its current y (vertical) and z (depth) positions.
        Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        // Update the camera's position to the newly calculated position.
        transform.position = newPosition;
    }
}
