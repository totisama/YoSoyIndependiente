using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed;
    private float dirX;
    private bool facingRight;
    private Vector3 localScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        localScale = transform.localScale;
    }

    public void MoveRight()
    {
        dirX = 1f;
        rb.velocity = Vector2.right * moveSpeed;
    }

    public void MoveLeft()
    {
        dirX = -1f;
        rb.velocity = Vector2.left * moveSpeed;
    }

    public void StopMoving()
    {
        dirX = 0f;
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (dirX > 0)
        {
            facingRight = true;
        }
        else if (dirX < 0)
        {
            facingRight = false;
        }

        if (facingRight && localScale.x < 0 || !facingRight && localScale.x > 0)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}
