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
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedy", agent.velocity.x);
        animator.SetFloat("speedx", agent.velocity.y);
        
        
    }
    public void onPlayLook(Vector3 position)
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 toOther = position - transform.position;
        if (Vector3.Dot(forward, toOther) < 0)
        {
            animator.SetBool("isLeft", true);
        }
        else
        {
            animator.SetBool("isLeft", false);
        }

    }
    public void onRandomLook()
    {
        animator.SetBool("isLeft",true);
        //Debug.Log("kukol som dolava");
        
        StartCoroutine(coroutine());
    }
    IEnumerator coroutine()
    {
        //Debug.Log("kuk sem");
        yield return new WaitForSeconds(Random.RandomRange(2,6));
        animator.SetBool("isLeft",false);
        //Debug.Log("kuk tam");
        yield return new WaitForSeconds(Random.RandomRange(2,6));
        onRandomLook();
    }

}
