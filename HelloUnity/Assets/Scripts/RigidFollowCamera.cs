using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target = null;
    public float vDist = 5.0f;
    public float hDist = 5.0f; 
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

            // Camera position is offset from the target position
            Vector3 eye = tPos - tForward * hDist + tUp * vDist;

            // The direction the camera should point is from the target to the camera position
            Vector3 cameraForward = tPos - eye;

            // Set the camera's position and rotation with the new values
            // This code assumes that this code runs in a script attached to the camera
            transform.position = eye;
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }
}
