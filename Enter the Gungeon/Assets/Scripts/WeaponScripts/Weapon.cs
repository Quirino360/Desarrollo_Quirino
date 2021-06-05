using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase abastracta
public class Weapon : MonoBehaviour
{
    //scripts that I need
    public GameBullet bulletScript;

    //Game Objects
    public GameObject weapon;
    public GameObject weaponSpite;



    // Weapon In Game Variables
    [SerializeField] private string weaponName = "Default";
    [SerializeField] private string Quality = "D";
    [SerializeField] private float DPS = 10.0f;
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private int AmmoCapacity = 10;
    [SerializeField] private float Damage = 10.0f;
    [SerializeField] private float FireRate = 10.0f;
    [SerializeField] private int shotSpeed = 10;
    [SerializeField] private int Range = 10;
    [SerializeField] private int Force = 10;
    [SerializeField] private int Spread = 10;

    // Variables


    void Start()
    {
        weapon = GameObject.Find("Weapon");
        weaponSpite = GameObject.Find("WeaponSprite");
    }

    
    void Update()
    {
        
    }

    public void ChangeWeaponRotation(Vector3 _dir, float _angle)
    {
        
        //Debug.Log(_dir.y);
        if(_dir.y <= 1.0f && _dir.y >= -1.0f)
        {
            weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, weapon.transform.eulerAngles.y, _angle);
            //Debug.Log("Weapon is in Left");
        }
        else if (_dir.y > 1.0f && _dir.y < -1.0f)
        {
            weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, weapon.transform.eulerAngles.y, _angle);
            //Debug.Log("Weapon is in Right");
        }
        /**/
    }

    public void Shoot(Vector3 _dir)
    {
        bulletScript.InstatiateBullet(weapon.transform.position, weapon.transform.rotation, _dir * shotSpeed);
    }

    
}
