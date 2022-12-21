using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentAnimation : MonoBehaviour
{
    public float movespeed;
    public bool isLookLeft;
    public NavMeshAgent agent;

    public Animator animator;
    public GameObject alerted;

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
            isLookLeft = true;
            Destroy(Instantiate(alerted,transform),1);
        }
        else
        {
            isLookLeft = false;
            Destroy(Instantiate(alerted, transform), 1);
        }
        animator.SetBool("isLeft", isLookLeft);

    }
    public void onRandomLook()
    {
        isLookLeft = !isLookLeft;
        animator.SetBool("isLeft",isLookLeft);
        //Debug.Log("flipujem kukuc");
        //Debug.Log(isLookLeft);

        StartCoroutine(coroutine());
    }

    public void onNameSaidBlind()
    {
        Destroy(Instantiate(alerted, transform), 1);
        isLookLeft = !isLookLeft;
        animator.SetBool("isLeft", isLookLeft);
        StartCoroutine(random());
    }

    public void onRattled()
    {
        Destroy(Instantiate(alerted, transform), 1);
    }
    IEnumerator coroutine()
    {
        //Debug.Log("kuk sem");
        yield return new WaitForSeconds(Random.RandomRange(2,6));
        animator.SetBool("isLeft",isLookLeft);
        onRandomLook();
    }

    IEnumerator random()
    {
        //Debug.Log("kuk sem");
        for(int i = 0; i<6; i++)
        {
            isLookLeft = !isLookLeft;
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("isLeft",isLookLeft);
        }

    }

}
