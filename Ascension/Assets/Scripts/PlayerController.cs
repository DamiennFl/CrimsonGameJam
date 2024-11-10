using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
<<<<<<< Updated upstream
    Animator animator; // animation shiii

    public float WalkSpeed = 5f;

    [SerializeField]
    private bool _isMoving = false; // default is set to false

    public bool IsMoving // Will help with the animation stuff
    {
=======
    TouchingDirections touchingDirections;
    Animator animator; // For animations and shit.

    public float WalkSpeed = 5f;
    public bool IsMoving 
    { 
>>>>>>> Stashed changes
        get
        {
            return _isMoving;
        }
<<<<<<< Updated upstream
        private set
=======
        set
>>>>>>> Stashed changes
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

<<<<<<< Updated upstream
    [SerializeField]
    private bool _isJumping = false; // default is set to false

=======
>>>>>>> Stashed changes
    public bool IsJumping
    {
        get
        {
            return _isJumping;
        }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        set
        {
            _isJumping = value;
            animator.SetBool("isJumping", value);
        }
<<<<<<< Updated upstream

    }



=======
    }

    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if (_isFacingRight != value)
            {
                // Flip the local scale
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        
        } }

    public float JumpImpulse = 10f;
>>>>>>> Stashed changes

    [SerializeField]
    private bool _isMoving = false; // Keeping track if the player is moving or not.

    [SerializeField]
    private bool _isJumping = false; // Keeping track if the player is jumping or not.

    public bool _isFacingRight = true;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Players status
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
=======
        touchingDirections = GetComponent<TouchingDirections>();
>>>>>>> Stashed changes
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Responds to the movement actions in Unity callback. 
    /// </summary>
    /// <param name="context">Movement callback.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
<<<<<<< Updated upstream
=======

        SetFacingDirection(moveInput);

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight) 
        {
            // face the left
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
            IsJumping = true;
        }
        else if(context.canceled)
        {
            IsJumping = false;
        }
>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * WalkSpeed, rb.linearVelocity.y);
    }
}
