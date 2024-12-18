using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target = null;
    public float vDist = 5.0f;
    public float hDist = 5.0f;
    private float MouseXOffset = 0.0f;
    private float MouseYOffset = 0.0f;
    public float rotationSpeed = 2.0f;
    public bool mouseControl = false; 

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

            // Camera position is offset from the target position
            Vector3 eye = tPos - tForward * hDist + tUp * vDist;

            // The direction the camera should point is from the target to the camera position
            Vector3 cameraForward = tPos - eye;

            if (mouseControl)
            {
                float MouseX = Input.GetAxis("Mouse X"); // set MouseX and MouseY to the current positions of the mouse.
                float MouseY = Input.GetAxis("Mouse Y");

                float horizontalInput = MouseX * rotationSpeed * Time.deltaTime;
                float verticalInput = -MouseY * rotationSpeed * Time.deltaTime;

                MouseXOffset += horizontalInput;
                MouseYOffset += verticalInput;

                Debug.Log("MouseX offset: " + MouseXOffset);
                Debug.Log("MouseY offset: " + MouseYOffset);
            }
            
            // Set the camera's position and rotation with the new values
            // This code assumes that this code runs in a script attached to the camera
            transform.position = eye;
            transform.rotation = Quaternion.LookRotation(cameraForward)
                                 * new Quaternion(Mathf.Sin(MouseYOffset * Mathf.Deg2Rad / 2), 0, 0, Mathf.Cos(MouseYOffset * Mathf.Deg2Rad / 2))
                                 * new Quaternion(0, Mathf.Sin(MouseXOffset * Mathf.Deg2Rad / 2), 0, Mathf.Cos(MouseXOffset * Mathf.Deg2Rad / 2)); ;
        }
    }
}
