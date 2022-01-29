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
        if (rb.velocity.normalized.x > 0)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.normalized.x < 0)
        {
            sr.flipX = true;
        }
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
