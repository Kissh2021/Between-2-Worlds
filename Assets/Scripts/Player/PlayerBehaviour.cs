using Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IDamageable, IClimber
{
    private enum MovingState
    {
        Normal,
        Climb
    }

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float climbSpeed = 3;
    [SerializeField]
    private float jumpPower = 10f;

    private int m_bonusJumps = 0;

    private Vector2 m_inputVector = Vector2.zero;

    private Rigidbody2D m_rb;
    private float origialGravityScale;

    public UnityEvent dieEvent;
    public UnityEvent bonusJumpEvent;

    private bool m_isgrounded;
    private MovingState _movingState = MovingState.Normal;

    private ClimbableBehavior currentClimbableBehavior;

    private float currentSpeed
    {
        get
        {
            switch (_movingState)
            {
                case MovingState.Climb:
                    return climbSpeed;
                default:
                    return speed;
            }
        }
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        origialGravityScale = m_rb.gravityScale;
    }

    void Update()
    {
        if(m_inputVector.magnitude > 0.1f)
        {
            Move(m_inputVector);
        } 
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
        if (_context.performed)
        {
            switch (_movingState)
            {
                case MovingState.Climb:
                    if(!m_isgrounded)
                    {
                        m_rb.gravityScale = origialGravityScale;
                    JumpFromLadder();
                    }
                    break;
                case MovingState.Normal:
                    if (m_isgrounded)
                    {
                        Jump();
                    }
                    else if (m_bonusJumps > 0)
                    {
                        Jump();
                        m_bonusJumps--;
                    }
                    break;
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

    void JumpFromLadder()
    {
        if (currentClimbableBehavior)
        {
            Vector2 jumpVector;
            if (Vector2.Dot(Vector2.right, transform.position - currentClimbableBehavior.transform.position) >= 0)
                jumpVector = new Vector2(1, 1);
            else
                jumpVector = new Vector2(-1, 1);

            m_rb.velocity = jumpVector * (jumpPower * 0.7f);
        }
    }

    private void Move(Vector2 _inputVector)
    {
        Vector2 vel = m_rb.velocity;
        vel.x = _inputVector.x * currentSpeed;

        if (_movingState == MovingState.Climb)
        {
            vel.y = _inputVector.y * currentSpeed;
        }

        m_rb.velocity = vel;
    }

    public void Warp(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            GameManager.instance.warp();
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
        m_rb.velocity = Vector2.zero;
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

    public void climb(ClimbableBehavior climbableBehavior)
    {
        currentClimbableBehavior = climbableBehavior;
        _movingState = MovingState.Climb;
        m_rb.gravityScale = 0f;
        Debug.Log($"Moving state : {_movingState}");
    }

    public void unclimb()
    {
        currentClimbableBehavior = null;
        _movingState = MovingState.Normal;
        m_rb.gravityScale = origialGravityScale;
        Debug.Log($"Moving state : {_movingState}");
    }
}
