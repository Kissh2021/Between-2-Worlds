using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float jumpPower = 10f;
    
    private Vector2 m_inputVector = Vector2.zero;

    private Rigidbody2D m_rb;

    public UnityEvent dieEvent;
    
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move(m_inputVector);
    }

    public void MoveInput(InputAction.CallbackContext _context)
    {
        m_inputVector = _context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            Vector2 vel = m_rb.velocity;
            vel.y = jumpPower;
            m_rb.velocity = vel;
        }
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
    public void Hit()
    {
        dieEvent.Invoke();
        StartCoroutine(waitBeforeDie());
    }

    private IEnumerator waitBeforeDie(float duration = 1f)
    {
        yield return new WaitForSecondsRealtime(duration);
        Destroy(gameObject);
    }
}
