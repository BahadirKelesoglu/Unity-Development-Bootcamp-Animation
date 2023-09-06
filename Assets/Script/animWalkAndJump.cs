using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animWalkAndJump : MonoBehaviour
{
    Animator anim;
    int walk, jump, strike;
    public float moveSpeed = 3f;
    public float rotationSpeed = 500f;
    public float strikeForce = 10f;
    public float jumpPower = 5f;
    private float waitStrike = 3f;
    private float waitJump = 3f;
    
    Rigidbody rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        walk = Animator.StringToHash("walking");
        jump = Animator.StringToHash("jumping");
        strike = Animator.StringToHash("Strike");
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        waitStrike += Time.deltaTime;
        waitJump += Time.deltaTime;
        Debug.Log(waitJump);


        bool walking = anim.GetBool(walk);
        bool jumping = anim.GetBool(jump);
        bool striking = anim.GetBool(strike);

        bool startJumping = Input.GetKey("space");
        bool startStriking = Input.GetMouseButton(0);


        //HAREKET 
        //float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, verticalInput);
        movement.Normalize(); 

        bool startWalking = movement.magnitude > 0.1f; 

        if(waitJump > 1.3f && waitStrike > 1.1f) { 
        transform.Translate(movement * moveSpeed *Time.deltaTime);
        }

        if (!walking && startWalking && verticalInput > 0)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("speed", 1f);
                      
        }
        
        else if(!walking && startWalking && verticalInput < 0)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("speed", -1f);
        }

        if (walking && !startWalking)
        {
            anim.SetBool("walking", false);
        }

        if (!jumping && startJumping)
        {
            
            anim.SetBool("jumping", true);
            waitJump = 0f;
            
        }

        if (waitJump >= 1.10f && waitJump <= 1.30f)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            Debug.Log("Jump happened");
           
        }

        if (jumping && !startJumping)
        {
            anim.SetBool("jumping", false);
        }

        if (!striking && startStriking)
        {
            anim.SetBool("Strike", true);
            waitStrike = 0f;

        }

        if(waitStrike >= 0.9f && waitStrike <1.1)

        {
            rb.AddForce(transform.forward * strikeForce, ForceMode.Impulse);
            
        }

        if (striking && !startStriking)
        {
            anim.SetBool("Strike", false);
        }


        
        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }





    }
}
