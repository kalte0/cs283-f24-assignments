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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            // Set the camera's position and rotation with the new values
            // This code assumes that this code runs in a script attached to the camera
            transform.position = actualPosition;
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }
}
