using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentAnimation : MonoBehaviour
{
    public float movespeed;
    public NavMeshAgent agent;

    public Animator animator;
    void Start()
    {
        // TODO: 
        // set random Animator Controller and a random sprite renderer color
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedy", agent.velocity.x);
        animator.SetFloat("speedx", agent.velocity.y);
        /*if (Input.GetKey("d"))
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
        }*/
    }
}
