using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        float xAxis = Input.GetAxis("Horizontal") * maxSpeed;
        float yAxis = Input.GetAxis("Vertical") * maxSpeed;

        // if braking
        if (Mathf.Abs(xAxis) <= Mathf.Epsilon)
        {
            xAxis = -rb.velocity.x;
        }
        if (Mathf.Abs(yAxis) <= Mathf.Epsilon)
        {
            yAxis = -rb.velocity.y;
        }


        Vector2 velToAdd = new Vector2(xAxis, yAxis);
        velToAdd = Vector2.ClampMagnitude(velToAdd, maxSpeed);
        rb.AddForce(velToAdd, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.AddForce(-(rb.velocity - rb.velocity.normalized * maxSpeed), ForceMode2D.Impulse);
        }
    }
}
