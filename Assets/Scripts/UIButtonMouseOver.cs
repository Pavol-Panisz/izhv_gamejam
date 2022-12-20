using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIButtonMouseOver : MonoBehaviour
{
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseLeave;

    private RectTransform myRectTr;
    private Vector3 bottomLeft; // corners in world space
    private Vector3 topRight;

    private bool wasHoveredLastFrame = false;

    private void Awake()
    {
        myRectTr = GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];
        myRectTr.GetWorldCorners(corners);

        bottomLeft = corners[0];
        topRight = corners[2];

        //Debug.Log(transform.name + " corners: " + bottomLeft + " " + topRight);
        
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //   Debug.Log(mouseWorldPos);

        bool isHovered = false;

        if (bottomLeft.x <= mouseWorldPos.x && mouseWorldPos.x <= topRight.x)
        {
            if (bottomLeft.y <= mouseWorldPos.y && mouseWorldPos.y <= topRight.y)
            {
                //Debug.Log("Hovering me " + transform.name);
                isHovered = true;
            }
        }

        if (isHovered && !wasHoveredLastFrame)
        {
            onMouseEnter.Invoke();
        } 
        else if (wasHoveredLastFrame && !isHovered)
        {
            onMouseLeave.Invoke();
        }

        wasHoveredLastFrame = isHovered;
    }
}
