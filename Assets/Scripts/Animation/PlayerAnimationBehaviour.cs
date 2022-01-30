using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerAnimationBehaviour : MonoBehaviour
{
    private Animator animator;
    private PlayerBehaviour pb;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    
    void Start()
    {
        pb = GetComponent<PlayerBehaviour>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0.3f)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < -0.3f)
        {
            sr.flipX = true;
        }
        animator.SetBool("IsGrounded", pb.isGrounded);
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
