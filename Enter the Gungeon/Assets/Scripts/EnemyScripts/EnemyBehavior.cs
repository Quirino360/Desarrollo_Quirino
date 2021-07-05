using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BEHAVIOR
{
    IDDLE = 0,
	SEEK, 
	FLEE, 
	ARRIVE, 
	PERSUIT, 
	EVADE,
	WANDER,
	PATH_FOLLOWING,
	COLLITION_AVOIDANCE,
	FLOCKING
};

public struct PathPoint
{
    public Vector3 position;
    public float radius;
};

public class EnemyBehavior : MonoBehaviour
{

    //Scripts that I need
    private Weapon EnemyWeaponScript;
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
    private float fireRate = 2;
    private float timer = 0;


    void Start()
    {
        behavior = BEHAVIOR.PERSUIT;
        
        EnemyWeaponScript = thisObj.GetComponentInChildren<Weapon>();

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(thisObj);
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
            //PlyrWeapon.weapon.transform.eulerAngles = new Vector3(PlyrWeapon.weapon.transform.eulerAngles.x, PlyrWeapon.weapon.transform.eulerAngles.y, angle);
            EnemyWeaponScript.ChangeWeaponRotation(directionVector, angle);
        }

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            EnemyWeaponScript.Shoot(directionVector);
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
            case BEHAVIOR.IDDLE:
                new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case BEHAVIOR.SEEK:
                SteeringBehaiviorSeek(_thisPostion, _targetPosition);
                break;
            case BEHAVIOR.FLEE:
                SteeringBehaiviorFlee(_thisPostion, _targetPosition);
                break;
            case BEHAVIOR.ARRIVE:
                SteeringBehaiviorArrival(_thisPostion, _targetPosition);
                break;
            case BEHAVIOR.PERSUIT:
                SteeringBehaiviorPersuit(_thisPostion, _targetPosition, _targetMovement);
                break;
            case BEHAVIOR.EVADE:
                SteeringBehaiviorEvade(_thisPostion, _targetPosition, _targetMovement);
                break;
            case BEHAVIOR.WANDER:
                SteeringBehaiviorWander(_thisPostion);
                break;
            case BEHAVIOR.PATH_FOLLOWING:
                SteeringBehaviorPathFollowing(_thisPostion);
                break;
            case BEHAVIOR.COLLITION_AVOIDANCE:
                new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case BEHAVIOR.FLOCKING:
                new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
    }    

    // ------------------------- Steering Behaviors ------------------------- //
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
    private Vector3 SteeringBehaiviorFlee(Vector3 _thisPostion, Vector3 _targetPosition)
    {
        desiredVelocity = NormalizeVector(_thisPostion - _targetPosition) * maxVelocity;
        steering = desiredVelocity - velocity;
        steering = NormalizeVector(steering) * maxForce;
        steering = steering / mass;
        velocity = NormalizeVector(velocity + steering) * maxSpeed;
        //playerPosition = playerPosition + velocity;
        return velocity;
    }
    private Vector3 SteeringBehaiviorArrival(Vector3 _thisPostion, Vector3 _targetPosition)
    {
        float r = 100;

        if (_thisPostion.x <= _targetPosition.x + r && _thisPostion.x + r >= _targetPosition.x && _thisPostion.y <= _targetPosition.y + r && _thisPostion.y + r >= _targetPosition.y)//inside
        {
            desiredVelocity = NormalizeVector(_targetPosition - _thisPostion) * (maxVelocity / 10);
        }
        else
        {
            desiredVelocity = NormalizeVector(_targetPosition - _thisPostion) * maxVelocity;
        }
        steering = desiredVelocity - velocity;
        velocity = velocity + steering;
        //playerPosition = playerPosition + velocity;
        return velocity;
    }
    private Vector3 SteeringBehaiviorWander(Vector3 _thisPostion) //Mejorar collition y la manera de hacerlo
    {
        
        if (Random.Range(0, 1000) > 990)
        {
            pathTarget = new Vector3(Random.Range(0.0f, 1060), Random.Range(0.0f, 700), 0);
            //std::cout << "TargetPos x = " << pathTarget.x << " TargetPos y = " << pathTarget.y << std::endl;
        }
        return SteeringBehaiviorSeek(_thisPostion, pathTarget);
    }
    private Vector3 SteeringBehaiviorPersuit(Vector3 _thisPostion, Vector3 _targetPosition, Vector3 _targetMovement) // 
    {
        float distanceDiference = DistanceBetweenVectors(_targetPosition, _thisPostion);
        //distanceDiference -= (_target.playerCircleShape.getRadius() + AI_CircleShape.getRadius());
        float T = distanceDiference / maxVelocity;
        Vector3 futurePosition = _targetPosition + _targetMovement * T;
        return SteeringBehaiviorSeek(_thisPostion, futurePosition); /**/
    }
    private Vector3 SteeringBehaiviorEvade(Vector3 _thisPostion, Vector3 _targetPosition, Vector3 _targetMovement)
    {
        float distanceDiference = DistanceBetweenVectors(_targetPosition, _thisPostion);
        float T = distanceDiference / maxVelocity;
        Vector3 futurePosition = _targetPosition + _targetMovement * T;
        return SteeringBehaiviorFlee(_thisPostion, futurePosition); /**/
    }

    private bool created = false;
    private Vector3 SteeringBehaviorPathFollowing(Vector3 _thisPostion)
    {
        if (created)
            CreateDefaultPath(_thisPostion);

        pathPointTarget = nodes[nodeID];

        if (DistanceBetweenVectors(_thisPostion, pathPointTarget.position) <= pathPointTarget.radius)
        {
            GetNextPointID();
        }

        return SteeringBehaiviorSeek(_thisPostion, pathPointTarget.position);
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
    float VectorLenght(Vector3 A) //magnitude
    {
        return Mathf.Sqrt((A.x * A.x) + (A.y * A.y));
    }

    // Path folowing ------------------------- 
    private void AddPathPoint(PathPoint _point) 
    {
        PathPoint[] pp_aux = nodes;
        nodes = new PathPoint[pp_aux.Length + 1];
        
        for (int i = 0; i < pp_aux.Length; i++)
        {
            nodes[i] = pp_aux[i];
        }
        nodes[nodes.Length] = _point;
    }
    private void CreateDefaultPath(Vector3 _thisPostion)
    {
        if (nodes == null)
            nodes = new PathPoint[10];

        float X = 0;
        float Y = 100;
        float radiusAux = 10; //75
        bool aux = false;
        PathPoint pp;



        for (int i = 0; i < 10; i++)
        {
            
            pp.position = new Vector3(_thisPostion.x + X, _thisPostion.y + Y, 0.0f);
            pp.radius = radiusAux;
            nodes[i] = pp;

            if (aux)
            {
                Y += 100;
                aux = false;
            }
            else
            {
                X += 100;
                aux = true;
            }
        }

        created = true;
    }
    private void SetPath(PathPoint[] newPathPoints)
    {
        nodes = newPathPoints;
    }
    private int GetNextPointID()
    {
        return 0;
    }

    // ------------------------- Getters ------------------------- //
    public BEHAVIOR GetBehavior() { return behavior; }
    public Vector3 GetVelocity() { return velocity; }

    // ------------------------- Getters ------------------------- //
    public void SetBehavior(BEHAVIOR _newBehavior) { behavior = _newBehavior; }

}

/*
 




class SteeringBehavior
{
public:


private:

	

public:

private:


};

 
 */