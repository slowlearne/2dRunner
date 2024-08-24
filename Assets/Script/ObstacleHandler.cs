using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    // Reference to the player
    public Transform player;
    // List of all obstacles in the scene
    public Transform[] obstacles;

    // The minimum distance that triggers the recycling
    public float recycleThreshold = 20f;

    void Update()
    {
        foreach (Transform obstacle in obstacles)
        {
            // Check if the obstacle is far enough behind the player to be recycled
            if (obstacle.position.x < player.position.x - recycleThreshold)
            {
                // Find the last obstacle in front of the player
                Transform lastObstacle = FindLastObstacle();

                int range = Random.Range(35, 45);                      //range of recycled obstacle to appear the distance from last obstacle.
                // Calculate the new position for the recycled obstacle
                float obstacleWidth = obstacle.GetComponent<Renderer>().bounds.size.x;
                Vector3 newPosition = new Vector3(lastObstacle.position.x + obstacleWidth + range, obstacle.position.y, obstacle.position.z);

                // Reposition the obstacle
                obstacle.position = newPosition;
            }
        }
    }

    Transform FindLastObstacle()
    {
        Transform lastObstacle = obstacles[0];
        foreach (Transform obstacle in obstacles)
        {
            if (obstacle.position.x > lastObstacle.position.x)
            {
                lastObstacle = obstacle;
            }
        }
        return lastObstacle;
    }

}
