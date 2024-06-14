using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = transform.position - target.position;
        offset = new Vector3(0, 3f, -5f);
    }

    private void Update()
    {
        
    }

    // Update is called once per frame 
    private void LateUpdate()
    {
        //transform.position = target.position + offset;

        Vector3 newPosition = new Vector3(target.position.x, target.position.y, target.position.z) + offset;
        //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.5f);
    }
}
