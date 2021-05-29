using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private Rigidbody2D rigidBody;

    private GameObject player;
    public Weapon PlyrWeapon;
    private Camera Plyrcamera;

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

    //Mouse position 
    float angle = 0;
    Vector3 directionVector;

    Vector3 mousePos;
    // Start is called before the first frame update
    void Awake()
    {
       
        playerInput = new PlayerInputActions();
        rigidBody = GetComponent<Rigidbody2D>();
        Plyrcamera = GetComponentInChildren<Camera>();
        
    }

    private void Start()
    {
        speed = speedMax;
        player = transform.Find("PlayerSprite").gameObject;
        

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    void Update()
    {
        Vector3 aux = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mousePos = Plyrcamera.ScreenToWorldPoint(aux);

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
    private void FixedUpdate()
    {
        Move();

        Vector3 lastDirection =  directionVector;
        directionVector = new Vector3(mousePos.x - rigidBody.position.x, mousePos.y - rigidBody.position.y, 0);
        directionVector.Normalize();
        if (directionVector !=  lastDirection)
        {
            angle = Mathf.Atan2(directionVector.y, directionVector.x) * 180.0f / Mathf.PI; //get the angle and make it degrees
            PlyrWeapon.weapon.transform.eulerAngles = new Vector3(PlyrWeapon.weapon.transform.eulerAngles.x, PlyrWeapon.weapon.transform.eulerAngles.y, angle);
        }

        //Debug.Log(angle);
    }

    // ------------------------------ Inputs ------------------------------//
    public void Move()
    {
        if (true == canMove)
        {
            Vector2 moveInput = playerInput.Movement.Move.ReadValue<Vector2>();
            rigidBody.velocity = moveInput * speed;
            //Debug.Log(moveInput * speed);
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
            PlyrWeapon.Shoot();
        }
        if (context.canceled)
        {
            //Debug.Log("Stoop Shoot");
        }
        
    }


}
