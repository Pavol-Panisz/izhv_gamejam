using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D agent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedY", agent.velocity.x);
        animator.SetFloat("speedX", agent.velocity.y);
    }
}
