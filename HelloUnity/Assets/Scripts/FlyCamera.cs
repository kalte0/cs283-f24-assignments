using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

// some code from : https://www.youtube.com/watch?v=PM8BZK3ig2s

public class FlyCamera : MonoBehaviour
{

    public float rotationSpeed = 800.0f;
    public float movementSpeed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Tour.interpolating)
        {
            if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
            {
                float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, verticalInput);
                transform.Rotate(Vector2.up, horizontalInput, Space.World);
            }

            if (Input.GetKey("w"))
            {
                gameObject.transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("s"))
            {
                gameObject.transform.Translate(0, 0, -movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                gameObject.transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey("d"))
            {
                gameObject.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            }
        }

    }
}
