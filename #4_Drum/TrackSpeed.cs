using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpeed : MonoBehaviour
{
    private Vector3 lastPosition;
    public float speed;
    
    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;
    }
}
