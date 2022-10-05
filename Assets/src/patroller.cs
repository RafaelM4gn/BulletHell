using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroller : MonoBehaviour
{
    float startPoint;
    float endPoint;
    float speed = 1.5f;
    bool direction = true;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position.z;
        endPoint = 15;
    }

    // Update is called once per frame
    void Update()
    {   
        if (transform.position.z < startPoint + endPoint && direction){
            transform.position = transform.position + Vector3.forward * Time.deltaTime * speed;
        } else {
            direction = false;
        }
        if (transform.position.z > startPoint  && !direction){
            transform.position = transform.position + Vector3.back * Time.deltaTime * speed;
        } else {
            direction = true;
        }

    }
}
