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
        bulletDirection =  _bulletDir;
        GameObject bulletCopy = Instantiate(bulletObj, _position, _rotation);
    }

    private void Move()
    {
        bulletObj.transform.position += bulletDirection;
    }

}
