using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float movementSpeed;
    public float gravity;
    public float gravityLimit;
    public float gravityMultiplier;
    public float jumpForce;
    private int doubleJumpCounter;
    public int doubleJumpMax;

    public  float cameraSpeed;
    
    Vector2 inputs;
    public CharacterController controller;

    public GameObject cam;
    public GameObject playerHead;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //  if (controller.isGrounded)
        // {
        //     gravity = 0;
        // }
        Movement();
        Rotation();
        Jump();

        if(controller.isGrounded == true)
        {
            doubleJumpCounter = doubleJumpMax;
        }
       
    }

    void Movement()
    {
       inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 movement = new Vector3(inputs.x, gravity, inputs.y);
        movement = Quaternion.Euler(0, cam.transform.eulerAngles.y,0) * movement;
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        playerHead.transform.rotation = Quaternion.Slerp(playerHead.transform.rotation, cam.transform.rotation, cameraSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (gravity < gravityLimit)
        {
            gravity = gravityLimit;
        }
        else
        {
            gravity -= Time.deltaTime * gravityMultiplier;
        }

        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            gravity = Mathf.Sqrt(jumpForce);
        }
        else if(controller.isGrounded == false && doubleJumpCounter >= 1 && Input.GetButtonDown("Jump"))
        {
             gravity = Mathf.Sqrt(jumpForce);
             doubleJumpCounter--;
        }

        
    }
    
}
