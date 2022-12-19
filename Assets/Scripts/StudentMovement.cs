using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 target;

    public bool reactsOnClick;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void Start()
    {
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
        agent.SetDestination(new Vector3(target.x, target.y, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (reactsOnClick) { SetTargetPositionClick(); }
        SetAgentPosition();
    }
}
