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

    public bool IsMoving { get; private set; }

    // Initialize components and set up starting velocity
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void Start()
    {
        // Initially set velocity to 0, just for testing. Remove if not needed.
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    /// <summary>
    /// Called when movement input is detected.
    /// </summary>
    /// <param name="context">Input context for movement.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // Check if movement started or stopped
        if (context.started)
        {
            IsMoving = true;
        }
        else if (context.canceled)
        {
            IsMoving = false;
        }

        // Update moveInput based on context
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    /// <summary>
    /// Called when jump input is detected.
    /// </summary>
    /// <param name="context">Input context for jump.</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        // Log whether the jump input action was performed
        Debug.Log("Jump input received: " + context.performed);

        // Check if jump action is performed and player is grounded
        if (context.performed && touchingDirections.IsGrounded)
        {
            Debug.Log("Jump action detected and player is grounded");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse); // Apply jump velocity
        }
        else if (!touchingDirections.IsGrounded)
        {
            Debug.Log("Cannot jump - Player is not grounded");
        }
    }

    // FixedUpdate is called at a fixed time interval
    private void FixedUpdate()
    {
        // Apply horizontal movement based on moveInput.x and current y velocity
        rb.linearVelocity = new Vector2(moveInput.x * WalkSpeed, rb.linearVelocity.y);

        if (!touchingDirections.IsGrounded && moveInput.x != 0 && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector2.down * 0.5f, ForceMode2D.Impulse);
        }
    }

    // Optional debugging for grounded status
    void Update()
    {
        // You can also use this for testing purposes to jump via space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump triggered via KeyCode"); // Check if this appears in the Console
            if (touchingDirections.IsGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
            }
            else
            {
                Debug.Log("Cannot jump - Player is not grounded");
            }
        }
    }

    // Ensure that the input system listens to the jump action
    private void OnEnable()
    {
        // Subscribe to the jump input action (this is if using Input System directly)
        var playerInput = GetComponent<PlayerInput>(); // Ensure PlayerInput component is attached
        playerInput.actions["Jump"].performed += OnJump; // Bind jump action to OnJump
    }

    private void OnDisable()
    {
        // Unsubscribe when the object is disabled
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Jump"].performed -= OnJump;
    }
}
