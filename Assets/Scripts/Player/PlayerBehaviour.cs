using System;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float jumpPower = 10f;

    private int m_bonusJumps = 0;
    
    private Vector2 m_inputVector = Vector2.zero;

    private Rigidbody2D m_rb;

    public UnityEvent dieEvent;
    public UnityEvent bonusJumpEvent;

    private bool m_isgrounded;
    
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move(m_inputVector);
    }

    private void FixedUpdate()
    {
        m_isgrounded = IsGrounded();
    }

    public void MoveInput(InputAction.CallbackContext _context)
    {
        m_inputVector = _context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext _context)
    {
        if (_context.performed )
        {
            if (m_isgrounded)
            {
                Jump();
            }
            else if(m_bonusJumps > 0)
            {
                Jump();
                m_bonusJumps--;
            }
        }
    }

    public void AddBonusJump(int amount = 1)
    {
        m_bonusJumps += amount;
        bonusJumpEvent.Invoke();
    }

    void Jump()
    {
        Vector2 vel = m_rb.velocity;
        vel.y = jumpPower;
        m_rb.velocity = vel;
    }
    
    private void Move(Vector2 _inputVector)
    {
        Vector2 vel = m_rb.velocity;
        vel.x = _inputVector.x*speed;
        m_rb.velocity = vel;
    }

    public void Warp(InputAction.CallbackContext _context)
    {
        if(_context.performed)
        {
            GameManager.instance.dm.warp();
        }
    }

    private bool IsGrounded()
    {
        bool grounded = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.2f);
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red);

        foreach (var hit in hits)
        {
            if (!hit.collider.CompareTag("Player"))
            {
                grounded = true;
            }
        }
        
        return grounded;
    }
    
    public void Hit()
    {
        GetComponent<PlayerInput>().enabled = false;
        m_rb.isKinematic = true;
        dieEvent.Invoke();
        StartCoroutine(waitBeforeDie());
    }

    private IEnumerator waitBeforeDie(float duration = 1f)
    {
        yield return new WaitForSecondsRealtime(duration);
        Destroy(gameObject);
    }
}
