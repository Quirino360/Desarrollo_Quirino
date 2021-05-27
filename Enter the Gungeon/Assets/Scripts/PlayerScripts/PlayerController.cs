using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private Rigidbody2D rigidBody;

    //Movement
    [SerializeField] private float speedMax = 10.0f;
    [SerializeField] private float speed = 0.0f;

    //Dash
    [SerializeField] private float dashSpeed = 1.5f;
    public bool invincible = false;
    bool canMove = true;
    bool canDash = true;
    float dashCooldown = 0.0f;
    float dashCooldownMax = 0.8f;

    //Shoot

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInputActions();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = speedMax;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (dashCooldown > 0.0f)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            canDash = true;
            invincible = false;
            dashCooldown = 0.0f;
        }
    }

    // ------------------------------ Inputs ------------------------------//
    public void Move()
    {
        if (true == canMove)
        {
            Vector2 moveInput = playerInput.Movement.Move.ReadValue<Vector2>();
            rigidBody.velocity = moveInput * speed;
        }
    }
    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            //Debug.Log("Dash");
            dashCooldown = dashCooldownMax;
            Vector2 moveInput = playerInput.Movement.Move.ReadValue<Vector2>();
            rigidBody.velocity = moveInput * speed * dashSpeed ;
            canDash = false;
            canMove = false;
            invincible = true;
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Shoot");
        }
        if (context.canceled)
        {
            //Debug.Log("Stoop Shoot");
        }
        
    }


}
