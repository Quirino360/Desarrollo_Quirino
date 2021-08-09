using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBullet : MonoBehaviour
{

    //Scripts I need 
    //Game Objects
    public GameObject thisObj;

    //Variables
    [SerializeField] public int Damage = 1;
    public Vector3 bulletDirection;

    private void Start()
    {

    }



    private void FixedUpdate()
    {
        Move();   
        
    }

    public void InstatiateBullet(Vector3 _position, Quaternion _rotation, Vector3 _bulletV)
    {
        bulletDirection = _bulletV;
        GameObject bulletCopy = Instantiate(thisObj, _position, _rotation);
        Destroy(bulletCopy, 5);
    }

    private void Move()
    {
        thisObj.transform.position += bulletDirection * Time.fixedDeltaTime;
    }

}
