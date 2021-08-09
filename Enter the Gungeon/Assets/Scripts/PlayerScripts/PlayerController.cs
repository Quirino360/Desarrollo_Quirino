using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Scripts that I need
    private PlayerInputActions playerInput;
    private Weapon PlyrWeaponScript;

    //Game Objects
    public GameObject thisObj;
    private Rigidbody2D rigidBody;
    private Camera Plyrcamera;
    private GameObject playerSprite;
    private Animator animator;
    public Rigidbody2D rigid_body { get { return rigidBody; } }
    

    //Movement
    [SerializeField] private float speed = 300.0f;

    //Dash
    [SerializeField] private float dashSpeed = 1.5f;
    public bool invincible = false;
    private bool canMove = true;
    private bool canDash = true;
    private bool canShoot = true;
    private float dashCooldown = 0.0f;
    private float dashCooldownMax = 0.8f;

    //Shoot
    private float angle = 0;
    private Vector3 directionVector;


    //Mouse position 
    Vector3 mousePos;

    //Player Color
    public Renderer CubeRenderer;

    // Start is called before the first frame update
    void Awake()
    {
       //scripts
        playerInput = new PlayerInputActions();
        PlyrWeaponScript = this.GetComponentInChildren<Weapon>();

        //PlyrWeaponScript = new AutomaticWeapon();

        //Game Objects
        rigidBody = GetComponent<Rigidbody2D>();
        Plyrcamera = GetComponentInChildren<Camera>();
        //playerSprite = GameObject.FindGameObjectWithTag("WeaponSprite");
        playerSprite = transform.Find("P_Sprite").gameObject;
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        //player color
        CubeRenderer = thisObj.GetComponent<Renderer>();

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


        //check for dash cooldown and restart the variables
        if (  dashCooldown > 0.0f)
        {
            // set player sprite red
            CubeRenderer.material.SetColor("_Color", Color.red);
            
            dashCooldown -= Time.deltaTime;
        }
        else if (dashCooldown <= 0.0f && canDash == false)
        {
            // set player sprite to normal
            CubeRenderer.material.SetColor("_Color", Color.white);

            //set cooldown to use variables
            canMove = true;
            canDash = true;
            canShoot = true;

            //set not insible 
            invincible = false;
            dashCooldown = 0.0f;

            //Debug.Log("Is in here");
        }

        //Animation
        animator.SetFloat("Horizontal", directionVector.x);
        animator.SetFloat("Vertical", directionVector.y);
        if (rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
            animator.SetFloat("Speed", 0);
        else
            animator.SetFloat("Speed", speed);

        //Debug.Log(PauseMenu.isPaused);
    }
    private void FixedUpdate()
    {
        Move();


        // ------------------------- Rotate Weapon ------------------------- //
        Vector3 lastDirection =  directionVector;
        directionVector = new Vector3(mousePos.x - rigidBody.position.x, mousePos.y - rigidBody.position.y, 0);
        directionVector.Normalize();
        //check if the direction has changed
        if (directionVector !=  lastDirection) 
        {
            angle = Mathf.Atan2(directionVector.y, directionVector.x) * 180.0f / Mathf.PI; //get the angle and make it degrees
            //PlyrWeapon.weapon.transform.eulerAngles = new Vector3(PlyrWeapon.weapon.transform.eulerAngles.x, PlyrWeapon.weapon.transform.eulerAngles.y, angle);
            PlyrWeaponScript.ChangeWeaponRotation(directionVector, angle);
            
        }
        //Debug.Log(directionVector);
    }

    // ------------------------------ Inputs ------------------------------//
    
    public void Move()
    {
        if (true == canMove && false == PauseMenu.isPaused)
        {
            Vector2 moveInput = playerInput.Movement.Move.ReadValue<Vector2>();
            rigidBody.velocity = moveInput * speed * Time.fixedDeltaTime;
            
            //Debug.Log(moveInput * speed);
        }
    }
    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && true == canDash && false ==PauseMenu.isPaused)
        {
            //dash speed
            Vector2 moveInput = playerInput.Movement.Move.ReadValue<Vector2>();
            rigidBody.velocity = moveInput * speed * dashSpeed * Time.fixedDeltaTime;

            //set cooldown
            dashCooldown = dashCooldownMax;

            //set variables to stop doing it
            canDash = false;
            canMove = false;
            canShoot = false;

            //set invisible
            invincible = true;




            //Debug.Log("Dash");
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && true == canShoot && false == PauseMenu.isPaused)
        {
            PlyrWeaponScript.Shoot(directionVector);
        }
        if (context.canceled)
        {
            //Debug.Log("Stoop Shoot");
        }
        
    }


}
