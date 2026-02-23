using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    Vector2 movementInput;
    Vector2 movementDirection;
    public float turnSpeed = 200f;
    private float turnInput;
    private bool slowMode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        movementInput = new Vector2(movementDirection.x, movementDirection.y);
        if (movementDirection != Vector2.zero)
        {
            MovePlayer(movementInput);
        }


        transform.Rotate(0f, 0f, -turnInput * turnSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void MovePlayer(Vector2 input)
    {
        Vector2 movement = input.normalized * moveSpeed * Time.deltaTime;

        transform.position += new Vector3(movement.x, movement.y, 0f);
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    public void OnSlowMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            slowMode = true;
            moveSpeed = 2f;
        }

        if (context.canceled)
        {
            slowMode = false;
            moveSpeed = 5f;
        }
    }

}
