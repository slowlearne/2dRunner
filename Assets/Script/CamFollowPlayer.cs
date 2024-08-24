using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        /*Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;*/

        Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
