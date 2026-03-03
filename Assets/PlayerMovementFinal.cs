using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementFinal : MonoBehaviour
{
  
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float speedModeMultiplier = 2f;
    [SerializeField] private float turnSpeed = 200f;

    private Vector2 moveInput;     
    private float turnInput;       
    private Rigidbody2D rb;
    private float currentMoveSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
      
        if (moveInput != Vector2.zero)
        {
            Vector2 move = moveInput * currentMoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }


        if (turnInput != 0f)
        {
            rb.rotation -= turnInput * turnSpeed * Time.fixedDeltaTime;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
        
    }

    public void OnSpeedMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentMoveSpeed = moveSpeed * speedModeMultiplier;
        }
        else if (context.canceled)
        {
            currentMoveSpeed = moveSpeed;
        }
    }


}