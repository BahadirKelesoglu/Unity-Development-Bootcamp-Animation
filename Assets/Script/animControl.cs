using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControl : MonoBehaviour
{
    Animator anim;
    bool isActive = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rotOpen();
            
        }
        isActive = !isActive;
    }

    private void rotOpen()
    {
        anim.SetBool("Active", !isActive);
        
    }
}
