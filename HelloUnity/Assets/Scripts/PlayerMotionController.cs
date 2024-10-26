using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public float movementVelocity = 5.0f;
    public float rotationVelocity = 5.0f;
    public Animator Animator = null; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController cc = gameObject.GetComponent<CharacterController>(); 
        if (Input.GetKey("w")) // forward
        { 
            cc.Move(gameObject.transform.TransformDirection(new Vector3(0, 0, movementVelocity * Time.deltaTime)));
        }
        else if (Input.GetKey("s")) // backward
        {
            cc.Move(gameObject.transform.TransformDirection(new Vector3(0, 0, -movementVelocity * Time.deltaTime)));
        }

        if (Input.GetKey("a")) // left
        {
            float theta = rotationVelocity * Mathf.Deg2Rad * Time.deltaTime; 
            gameObject.transform.Rotate(0, -rotationVelocity * Time.deltaTime, 0);
        }
        else if (Input.GetKey("d")) // right
        {
            float theta = rotationVelocity * Mathf.Deg2Rad * Time.deltaTime;
            gameObject.transform.Rotate(0, rotationVelocity * Time.deltaTime, 0);
        }

        if (Input.GetKey("w") | Input.GetKey("s") | Input.GetKey("a") | Input.GetKey("d"))
        {
            Animator.Play("Run");
        }
        else
        {
            Animator.Play("Idle");
        }

    }
}
