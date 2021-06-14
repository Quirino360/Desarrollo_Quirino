using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBullet : MonoBehaviour
{

    //Scripts I need 


    //Game Objects
    public GameObject bulletObj;

    //Variables
    [SerializeField] public int Damage = 1;
    public Vector3 bulletDirection;
    private float bulletBaseSpeed = 1.0f;
    private void Start()
    {

    }

    void Update()
    {
        Move();
        Destroy(bulletObj, 5);
    }

    public void InstatiateBullet(Vector3 _position, Quaternion _rotation, Vector3 _bulletDir)
    {
        _bulletDir.Normalize();
        bulletDirection =  _bulletDir * bulletBaseSpeed;
        GameObject bulletCopy = Instantiate(bulletObj, _position, _rotation);
    }

    private void Move()
    {
        bulletObj.transform.position += bulletDirection;
    }

}
