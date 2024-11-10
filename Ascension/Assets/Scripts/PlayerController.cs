using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    public float WalkSpeed = 5f;
    public bool IsMoving { get; set; }
    public float JumpImpulse = 10f;

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
        touchingDirections = GetComponent<TouchingDirections>();
    }

    /// <summary>
    /// Responds to the movement actions in Unity callback. 
    /// </summary>
    /// <param name="context">Movement callback.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsMoving = true;
        }
        else if (context.canceled)
        {
            IsMoving = false;
        }
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * WalkSpeed, rb.linearVelocity.y);
    }
}
