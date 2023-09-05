using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animWalkAndJump : MonoBehaviour
{
    Animator anim;
    int walk, jump, strike;
    float moveSpeed = 3f;
    float rotationSpeed = 500f;
    float strikeForce = 10f;
    float waitStrike = 3f;
    float waitJump = 3f;
    float jumpPower = 5f;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize(); 

        bool startWalking = movement.magnitude > 0.1f; 
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (!walking && startWalking)
        {
            anim.SetBool("walking", true);
            
            
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
            rb.AddForce(Vector3.up * jumpPower);
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
            rb.AddForce(transform.forward * strikeForce);
            
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
