using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentAnimation : MonoBehaviour
{
    public float movespeed;
    public bool isLookRight;
    public NavMeshAgent agent;

    public Animator animator;

    IEnumerator coroutinecka;
    void Start()
    {
        onRandomLook();
        Debug.Log("idzeme rotatovat");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedy", agent.velocity.x);
        animator.SetFloat("speedx", agent.velocity.y);
        
        
    }
    public void onPlayLook(Vector3 position,bool isLeft)
    {
        if (isLeft)
        {
            animator.SetBool("isLeft",true);
            Debug.Log("left look");
        }
        else
        {
            animator.SetBool("isLeft", false);
            Debug.Log("right look");
        }
        
    }
    public void onRandomLook()
    {
        animator.SetBool("isLeft",true);
        Debug.Log("kukol som dolava");
        
        StartCoroutine(coroutine());
    }
    IEnumerator coroutine()
    {
        Debug.Log("halooo");
        Debug.Log("kuk sem");

        yield return new WaitForSeconds(Random.RandomRange(10,50));
        animator.SetBool("isLeft",false);
        yield return new WaitForSeconds(Random.RandomRange(10, 50));
        onRandomLook();
    }

}
