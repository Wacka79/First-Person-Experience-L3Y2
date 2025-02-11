using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float movementSpeed;
    float sprintSpeed;
    float walkSpeed;

    [Header("Jump")]
    public float gravity;
    public float gravityLimit;
    public float gravityLimitCon;
    private float gravityMultiplier;
    public float gravityMultiplierCon;
    public float jumpForce;
    private int doubleJumpCounter;
    public int doubleJumpMax;

    [Header("Miscellaneous")]
    public  float cameraSpeed;
    Vector2 inputs;
    public CharacterController controller;
    public GameObject cam;
    public GameObject playerHead;
    public Animator bbox;
    public string spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sprintSpeed = movementSpeed * 2;
        walkSpeed = movementSpeed;
        gravityMultiplier = gravityMultiplierCon;
        gravityLimit = gravityLimitCon;
        //controller = GameObject.Find("Player").GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera");
        playerHead = GameObject.Find("Player").transform.GetChild(0).GetChild(0).gameObject;
        //bbox = GameObject.Find("Canvas").transform.GetChild(6).GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20f)
        {
            StartCoroutine(ResetOnDeath());
        }
       
        Movement();
        Rotation();
        Jump();
        Sprint();
        Glide();

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
    void Sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift) && controller.isGrounded == true)
        {
            movementSpeed = sprintSpeed;
            //gravityMultiplier = gravityMultiplierCon + 3;
        }
        else
        {
            movementSpeed = walkSpeed;
            //gravityMultiplier = gravityMultiplierCon;
        }
    }

    void Glide()
    {
        if(controller.isGrounded == false && Input.GetKey(KeyCode.Space)) // check if player is in the air and holding space, change gravity
        {
            //gravityMultiplier = gravityMultiplier / 6;
            gravityLimit = gravityLimitCon / 40;
        }
        else
        {
            //gravityMultiplier = gravityMultiplierCon;
            gravityLimit = gravityLimitCon;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(ResetOnDeath());
        }
    }
    

    public IEnumerator ResetOnDeath()
    {
        bbox.SetBool("Out", false);
        controller.enabled = false;
        yield return new WaitForSeconds(1f);

        transform.position = GameObject.Find(spawnPoint).transform.position;
        yield return new WaitForSeconds(.1f);
        bbox.SetBool("Out", true);
        controller.enabled = true;
        
    }

    
    public IEnumerator ResetPos()
    {
        bbox.SetBool("Out", true);
        controller.enabled = false;
        transform.position = GameObject.Find(spawnPoint).transform.position;
        yield return new WaitForSeconds(.1f);
        controller.enabled = true;
        
    }

    public IEnumerator LoadNewScene(string levelName)
    {
        bbox.SetBool("Out", false);
        controller.enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
        
        
    }

    
}
