using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupCamera : MonoBehaviour
{
    public float speed = 2.0f;
    

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
