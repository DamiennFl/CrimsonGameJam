using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;

    public float WalkSpeed = 5f;
    public bool IsMoving { get; set; }


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
    }

    /// <summary>
    /// Responds to the movement actions in Unity callback. 
    /// </summary>
    /// <param name="context">Movement callback.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * WalkSpeed, rb.linearVelocity.y);
    }
}
