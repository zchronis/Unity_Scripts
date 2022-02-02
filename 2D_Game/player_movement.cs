using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float move_speed;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 move_direction;
    private bool is_attacking;

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", move_direction.x);
        animator.SetFloat("Vertical", move_direction.y);
        animator.SetFloat("Speed", move_direction.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (!GetComponent<player_combat>().attacking)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

    }

    void ProcessInputs()
    {
        float movement_x = Input.GetAxisRaw("Horizontal");
        float movement_y = Input.GetAxisRaw("Vertical");

        move_direction = new Vector2(movement_x, movement_y).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(move_direction.x * move_speed, move_direction.y * move_speed);
    }
}
