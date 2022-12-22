using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 target;

    public bool reactsOnClick;

    private float defaultWalkSpeed = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; 
    }

    public void Start()
    {
        defaultWalkSpeed = agent.speed;
        SetTargetPosition(transform.position);
    }

    public void SetTargetPosition(Vector3 pos)
    {
        target = pos;

    }

    private void SetTargetPositionClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector3(tmp.x, tmp.y, 0f);
        } 
    }

    void SetAgentPosition()
    {
        try
        {
            agent.SetDestination(new Vector3(target.x, target.y, 0f));
        } catch
        {
            Debug.Log("unsuccesfully set agent destination with targer " + 
                target.x + " " + target.y);
        }
    }

    public void Warp(Vector3 pos)
    {
        agent.Warp(pos);
    }

    void Update()
    {
        if (reactsOnClick) { SetTargetPositionClick(); }
        SetAgentPosition();

        
    }

    public void SetWalkSpeedToRun()
    {
        agent.speed = defaultWalkSpeed * 3f;
    }

    public void SetWalkSpeedToDefault()
    {
        agent.speed = defaultWalkSpeed;
    }
}
