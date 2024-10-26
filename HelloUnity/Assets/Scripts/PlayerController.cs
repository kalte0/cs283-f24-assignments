using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public static float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private static int score = 0; 

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded; 
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f; 
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed); 
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move; 
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public static void IncrementScore()
    {
        score++; 
    }

    public static int GetScore()
    {
        return score; 
    }

    public static int GetLevel()
    {
        return (int)score / 3;
    }
   
    
}
