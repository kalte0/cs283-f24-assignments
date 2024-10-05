using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerController1 : MonoBehaviour
{

    public float movementVelocity = 5.0f;
    public float rotationVelocity = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) // forward
        {
            // gameObject.transform.Translate() += Vector3.forward * movementVelocity * Time.deltaTime;
            gameObject.transform.Translate(0, 0, movementVelocity * Time.deltaTime);
        }
        else if (Input.GetKey("s")) // backward
        {
            // gameObject.transform.position -= Vector3.forward * movementVelocity * Time.deltaTime; 
            gameObject.transform.Translate(0, 0, -movementVelocity * Time.deltaTime);
        }
        if (Input.GetKey("a")) // left
        {
            float theta = rotationVelocity * Mathf.Deg2Rad * Time.deltaTime; // 3 degrees in Radians. 
            //gameObject.transform.localRotation *= new Quaternion(0, Mathf.Sin(-theta/2), 0, Mathf.Cos(-theta / 2));
            gameObject.transform.Rotate(0, -rotationVelocity*Time.deltaTime, 0); 
        }
        else if (Input.GetKey("d")) // right
        {
            float theta = rotationVelocity * Mathf.Deg2Rad * Time.deltaTime; // 3 degrees in Radians.
            //gameObject.transform.localRotation *= new Quaternion(0, Mathf.Sin(theta / 2), 0, Mathf.Cos(theta / 2));
            gameObject.transform.Rotate(0, rotationVelocity * Time.deltaTime, 0);
        }
    }
}
