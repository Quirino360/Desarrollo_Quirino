using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    //Scripts that I need
    [SerializeField]private Weapon minigun;
    [SerializeField]private Weapon ak;
    [SerializeField]private Weapon gun01;
    private PlayerController playerControllerScript;
    //rigidbody.Velocity

    //Game Objects
    public GameObject thisObj;
    private GameObject playerObj;

    //Steering Behavior
    private Vector3 velocity, desiredVelocity, steering; //used in seek, flee
    [SerializeField] private BEHAVIOR behavior = BEHAVIOR.IDDLE;
    [SerializeField] private float health = 3.0f;
    private float maxVelocity = 2.0f;
    private float maxSpeed = 2.0f;
    private float maxForce = 1.0f;
    private float mass = 1.0f;

    // Wander ------------------------- 
    private Vector3 pathTarget;

    // Path folowing ------------------------- 
    private PathPoint[] nodes = new PathPoint[10];
    int nodeID = 0;
    PathPoint pathPointTarget;

    // Where to shoot 
    private float angle = 0;
    private Vector3 directionVector;

    //Fire rate
    [SerializeField] private float fireRate = 2;
    private float timer = 0;


    void Start()
    {
        behavior = BEHAVIOR.PERSUIT;

        //EnemyWeaponScript = thisObj.GetComponentInChildren<Weapon>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(thisObj);
            SceneManager.LoadScene("Dungeon");
        }

    }

    private void FixedUpdate()
    {

        Vector3 lastDirection = directionVector;
        directionVector = new Vector3(playerObj.transform.position.x - thisObj.transform.position.x, playerObj.transform.position.y - thisObj.transform.position.y, 0);
        directionVector.Normalize();
        //check if the direction has changed
        if (directionVector != lastDirection)
        {
            angle = Mathf.Atan2(directionVector.y, directionVector.x) * 180.0f / Mathf.PI; //get the angle and make it degrees
            minigun.ChangeWeaponRotation(directionVector, angle);
            ak.ChangeWeaponRotation(directionVector, angle);
            gun01.ChangeWeaponRotation(transform.forward, angle);


        }

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            minigun.Shoot(directionVector);
            ak.Shoot(transform.forward * 1);
            gun01.Shoot(transform.forward * 1);
            timer = 0;
        }

        UpdateMovement(thisObj.transform.position, playerControllerScript.rigid_body.velocity, playerControllerScript.thisObj.transform.position);
        thisObj.transform.position += GetVelocity() * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            //Debug.Log("Enemy life = " + health);
        }
    }






    // -------------------- Steering Behaviors
    //Depending on the steering directions it returns the direction
    public void UpdateMovement(Vector3 _thisPostion, Vector3 _targetMovement, Vector3 _targetPosition)
    {
        switch (behavior)
        {
            case BEHAVIOR.PERSUIT:
                SteeringBehaiviorPersuit(_thisPostion, _targetPosition, _targetMovement);
                break;
        }
    }

    private Vector3 SteeringBehaiviorSeek(Vector3 _thisPostion, Vector3 _targetPosition)
    {
        desiredVelocity = NormalizeVector(_targetPosition - _thisPostion) * maxVelocity;
        steering = desiredVelocity - velocity;
        if (steering == new Vector3(0.0f, 0.0f, 0.0f))
            steering = desiredVelocity - new Vector3(velocity.x / 2.0f, velocity.y / 2.0f);
        steering = NormalizeVector(steering) * maxForce;
        steering = steering / mass;
        velocity = NormalizeVector(velocity + steering) * maxSpeed;
        //playerPosition = playerPosition + velocity;
        return velocity;
    }

    private Vector3 SteeringBehaiviorPersuit(Vector3 _thisPostion, Vector3 _targetPosition, Vector3 _targetMovement) // 
    {
        float distanceDiference = DistanceBetweenVectors(_targetPosition, _thisPostion);
        //distanceDiference -= (_target.playerCircleShape.getRadius() + AI_CircleShape.getRadius());
        float T = distanceDiference / maxVelocity;
        Vector3 futurePosition = _targetPosition + _targetMovement * T;
        return SteeringBehaiviorSeek(_thisPostion, futurePosition); /**/
    }

    // MathFunctions ------------------------- 
    private Vector3 NormalizeVector(Vector3 A)
    {
        float v = Mathf.Sqrt((A.x * A.x) + (A.y * A.y) + (A.z * A.z));
        return new Vector3(A.x / v, A.y / v);
    }
    private Vector3 TruncateVector(Vector3 A, float x)
    {
        return NormalizeVector(A) * x;
    }
    private float DistanceBetweenVectors(Vector3 A, Vector3 B)
    {
        float a = A.x - B.x;
        float b = A.y - B.y;
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    // ------------------------- Getters ------------------------- //
    public BEHAVIOR GetBehavior() { return behavior; }
    public Vector3 GetVelocity() { return velocity; }

    // ------------------------- Getters ------------------------- //
    public void SetBehavior(BEHAVIOR _newBehavior) { behavior = _newBehavior; }
}
