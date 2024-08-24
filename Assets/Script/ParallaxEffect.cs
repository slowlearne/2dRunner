using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallexEffect;
    void Start()
    {
        startpos = transform.position.x;                           // Store the initial x position of the object.
        length = GetComponent<SpriteRenderer>().bounds.size.x;                   //Calculate the length (width) of the sprite using the SpriteRenderer component.
    }
    void FixedUpdate()
    {
        // Calculate the camera's movement (temp) in relation to the parallax effect.
        // This determines how far the background should move to create the parallax illusion.
        float temp = (cam.transform.position.x * (1 - parallexEffect));

        // Calculate the distance the background should move based on the parallax effect.
        float dist = (cam.transform.position.x * parallexEffect);

        // Update the position of the object to create the parallax effect.
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // Checking if the camera has moved past the length of the sprite.
        // If so, reposition the sprite to create a looping background effect.
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
