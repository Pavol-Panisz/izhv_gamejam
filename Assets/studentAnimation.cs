using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class studentAnimation : MonoBehaviour
{
    public float movespeed;

    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedy", 0);
        animator.SetFloat("speedx", 0);
        if (Input.GetKey("d"))
        {
            animator.SetFloat("speedy",2);
            transform.position = transform.position + transform.right * movespeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            animator.SetFloat("speedy", -2);
            transform.position = transform.position - transform.right * movespeed * Time.deltaTime;
        }
        if (Input.GetKey("w"))
        {
            animator.SetFloat("speedx", 2);
            transform.position = transform.position + transform.up * movespeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            animator.SetFloat("speedx", -2);
            transform.position = transform.position - transform.up * movespeed * Time.deltaTime;
        }
    }
}
