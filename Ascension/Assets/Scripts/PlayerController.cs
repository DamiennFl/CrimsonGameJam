using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    // Input fields and components
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;

    // Movement and jump settings
    public float WalkSpeed = 5f;
    public float JumpImpulse = 10f;

    [SerializeField]
    private bool _isMoving = false; // Keeping track if the player is moving or not.

    [SerializeField]
    private bool _isJumping = false; // Keeping track if the player is jumping or not.

    public bool _isFacingRight = true;

    Animator animator; // For animations.

    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    public bool IsJumping
    {
        get { return _isJumping; }
        set
        {
            _isJumping = value;
            animator.SetBool("isJumping", value);
        }
    }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                // Flip the local scale
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    // Initialize components
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            Debug.Log("Jump action detected and player is grounded");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
            IsJumping = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * WalkSpeed, rb.linearVelocity.y);

        // Apply additional downward force if needed
        if (!touchingDirections.IsGrounded && moveInput.x != 0 && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector2.down * 0.5f, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        HandleLanding();

        // Testing jump via space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump triggered via KeyCode");
            if (touchingDirections.IsGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
                IsJumping = true;
            }
            else
            {
                Debug.Log("Cannot jump - Player is not grounded");
            }
        }
    }

    private void HandleLanding()
    {
        // Only reset IsJumping when the player is grounded and falling or has landed
        if (IsJumping && touchingDirections.IsGrounded && rb.linearVelocity.y <= 0)
        {
            IsJumping = false;
        }
    }

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Jump"].performed -= OnJump;
    }
}
