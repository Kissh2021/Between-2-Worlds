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
    [SerializeField]
    private Vector2 _jumpVector = new Vector2(0.8f, 1);
    [SerializeField]
    private GameObject stepParticle;
    [SerializeField]
    private float stepInterval = 0.5f;
    [SerializeField]
    private GameObject dieParticle;

    private int m_bonusJumps = 0;

    private Vector2 m_inputVector = Vector2.zero;

    public Vector2 inputVector
    {
        get { return m_inputVector; }
    }

    private Rigidbody2D m_rb;
    private float origialGravityScale;

    public UnityEvent dieEvent = new UnityEvent();
    public UnityEvent bonusJumpEvent = new UnityEvent();

    private bool m_isgrounded;
    public bool isGrounded
    {
        get { return m_isgrounded; }
    }

    private MovingState _movingState = MovingState.Normal;

    private ClimbableBehavior currentClimbableBehavior;

    private bool stopMoveInput = false;

    private PlayerInput m_playerInput;
    private SpriteRenderer m_renderer;
    private Animator m_animator;

    private bool stepParticleAllowed = true;

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
        m_animator = GetComponentInChildren<Animator>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_playerInput = GetComponent<PlayerInput>();
        m_rb = GetComponent<Rigidbody2D>();
        origialGravityScale = m_rb.gravityScale;

        GameManager.instance.dm.warpEvent.AddListener(setAnimatorLayer);
        setAnimatorLayer();
    }

    void Update()
    {
        if (!stopMoveInput)
        {
            Move(m_inputVector);
        }

        Debug.Log(m_rb.velocity.magnitude);
        if (stepParticleAllowed && m_isgrounded && m_rb.velocity.magnitude >= 0.1f)
            createStepParticle();
    }

    private void FixedUpdate()
    {
        m_isgrounded = IsGrounded();
    }

    public void MoveInput(InputAction.CallbackContext _context)
    {
        stopMoveInput = false;
        m_inputVector = _context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            switch (_movingState)
            {
                case MovingState.Climb:
                    if (!m_isgrounded)
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
            stopMoveInput = true;
            Vector2 jumpVector;
            if (Vector2.Dot(Vector2.right, transform.position - currentClimbableBehavior.transform.position) >= 0)
                jumpVector = new Vector2(_jumpVector.x, _jumpVector.y);
            else
                jumpVector = new Vector2(-(_jumpVector.x), _jumpVector.y);

            m_rb.velocity = jumpVector * (jumpPower * 0.6f);
        }
    }

    private void Move(Vector2 _inputVector)
    {
        Vector2 vel = m_rb.velocity;
        vel.x = _inputVector.x * currentSpeed;

        if (_movingState == MovingState.Climb)
        {
            vel.y = _inputVector.y * currentSpeed;
            vel.x = Mathf.Clamp(vel.x, -speed, speed);
            vel.y = Mathf.Clamp(vel.y, -speed, speed);
        }
        else
        {
            vel.x = _inputVector.x * currentSpeed;

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
        disable();
        StartCoroutine(waitBeforeDie());
    }

    private IEnumerator waitBeforeDie(float duration = 1f)
    {
        GameObject particle = Instantiate(dieParticle);
        particle.transform.position = gameObject.transform.position;
        yield return new WaitForSecondsRealtime(duration);
        reset();
        dieEvent.Invoke();
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

    public void disable()
    {
        m_rb.velocity = Vector2.zero;
        m_playerInput.enabled = false;
        m_rb.isKinematic = true;
        m_renderer.enabled = false;
    }

    public void reset()
    {
        m_bonusJumps = 0;
        _movingState = MovingState.Normal;
        stopMoveInput = false;
        currentClimbableBehavior = null;
        m_playerInput.enabled = true;
        m_rb.isKinematic = false;
        m_renderer.enabled = true;
    }

    private void setAnimatorLayer()
    {
        if(GameManager.instance.dm.dimension == DimensionsManager.Dimensions.Afthlea)
        {
            m_animator.SetLayerWeight(0, 1);
            m_animator.SetLayerWeight(1, 0);
        } else
        {
            m_animator.SetLayerWeight(0, 0);
            m_animator.SetLayerWeight(1, 1);
        }
    }
    
    private void createStepParticle()
    {
        GameObject particle = Instantiate(stepParticle);
        particle.transform.position = gameObject.transform.position;
        StartCoroutine(stepParticleCoroutine());
    }

    private IEnumerator stepParticleCoroutine()
    {
        stepParticleAllowed = false;
        yield return new WaitForSeconds(stepInterval);
        stepParticleAllowed = true;
    }
}
