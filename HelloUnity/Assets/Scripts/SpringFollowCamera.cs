using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    public Transform target = null;
    public float vDist = 2.0f;
    public float hDist = 2.0f;
    public float springConstant = 1.0f;
    public float dampConstant = 1.0f;
    private Vector3 velocity = new Vector3(0, 0, 0);
    public float rotationSpeed = 2.0f;
    private float MouseXOffset = 0.0f;
    private float MouseYOffset = 0.0f;
    public bool mouseControl = false;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("collision");
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("collision leave"); 
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            MouseXOffset = 0.0f;
            MouseYOffset = 0.0f; 
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // tPos, tUp, tForward = Position, up, and forward vector of target
            // hDist = horizontal follow distance
            // vDist = vertical follow distance
            Vector3 tPos = target.transform.position;
            Vector3 tUp = target.transform.up;
            Vector3 tForward = target.transform.forward;
            Vector3 actualPosition = transform.position; 

            // Camera position is offset from the target position
            Vector3 idealEye = tPos - tForward * hDist + tUp * vDist;

            // The direction the camera should point is from the target to the camera position
            Vector3 cameraForward = tPos - actualPosition;

            // Compute the acceleration of the spring, and then integrate
            Vector3 displacement = actualPosition - idealEye;
            Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

            // Update the camera's velocity based on the spring acceleration
            velocity += springAccel * Time.deltaTime;
            actualPosition += velocity * Time.deltaTime;

            if (mouseControl)
            {
                float MouseX = Input.GetAxis("Mouse X"); // set MouseX and MouseY to the current positions of the mouse.
                float MouseY = Input.GetAxis("Mouse Y");

                float horizontalInput = MouseX * rotationSpeed * Time.deltaTime;
                float verticalInput = -MouseY * rotationSpeed * Time.deltaTime;

                MouseXOffset += horizontalInput;
                MouseYOffset += verticalInput;
            }

            // Set the camera's position and rotation with the new values
            // This code assumes that this code runs in a script attached to the camera
            transform.position = actualPosition;
            transform.rotation = Quaternion.LookRotation(cameraForward) 
                                * new Quaternion(Mathf.Sin(MouseYOffset * Mathf.Deg2Rad / 2), 0, 0, Mathf.Cos(MouseYOffset * Mathf.Deg2Rad / 2))
                                * new Quaternion(0, Mathf.Sin(MouseXOffset * Mathf.Deg2Rad / 2) , 0, Mathf.Cos(MouseXOffset * Mathf.Deg2Rad / 2));
            //transform.Rotate(Vector3.right, verticalInput);
            //transform.Rotate(Vector2.up, horizontalInput, Space.World);
        }

        
    }
}
