using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    //Scripts that I need
    public Weapon EnemyWeaponScript;

    //Game Objects
    public GameObject thisObj;
    private GameObject playerObj; 

    //
    private float health = 3.0f;

    // Where to shoot 
    private float angle = 0;
    private Vector3 directionVector;

    //Fire rate
    private float fireRate = 2;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        //thisObj = transform.Find("Enemy").gameObject;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            Debug.Log("Enemy = " + health);
        }
    }
}
