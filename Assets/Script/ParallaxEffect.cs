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
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

    /*public float depth;
    public PlayerController playerObj;

    private void Update()
    {
        float backgroundSpeed = playerObj.velocity.x / depth;
        Vector2 bgPosition = transform.position;

        bgPosition.x -= backgroundSpeed * Time.deltaTime;          //moving background -x axis to make as player is moving forward.

        if (bgPosition.x <= -19)
            bgPosition.x = 31;

        transform.position = bgPosition;
    }*/
}
