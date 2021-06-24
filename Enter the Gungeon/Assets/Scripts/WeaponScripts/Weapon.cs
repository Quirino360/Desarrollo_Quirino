using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase abastracta
public class Weapon : MonoBehaviour
{
    //scripts that I need
    public GameBullet bulletScript;

    //Game Objects
    public GameObject weaponObj;
    public GameObject Muzzle;
    private GameObject weaponSprite;



    // Weapon In Game Variables
    [SerializeField] private string weaponName = "Default";
    [SerializeField] private string Quality = "D";
    [SerializeField] private float DPS = 10.0f;
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private int AmmoCapacity = 10;
    [SerializeField] private float Damage = 10.0f;
    [SerializeField] private float FireRate = 10.0f;
    [SerializeField] private float shotSpeed = 10.0f;
    [SerializeField] private int Range = 10;
    [SerializeField] private int Force = 10;
    [SerializeField] private int Spread = 10;

    // Variables


    void Start()
    {
        weaponSprite = GameObject.Find("WeaponSprite");
    }

    
    void Update()
    {
        
    }

    public void ChangeWeaponRotation(Vector3 _dir, float _angle)
    {
        //Debug.Log(_dir.y);
        if(_dir.y <= 1.0f && _dir.y >= -1.0f)
        {
            weaponObj.transform.eulerAngles = new Vector3(weaponObj.transform.eulerAngles.x, weaponObj.transform.eulerAngles.y, _angle);
            //Debug.Log("Weapon is in Left");
        }
        else if (_dir.y > 1.0f && _dir.y < -1.0f)
        {
            weaponObj.transform.eulerAngles = new Vector3(weaponObj.transform.eulerAngles.x, weaponObj.transform.eulerAngles.y, _angle);
            //Debug.Log("Weapon is in Right");
        }
        /**/
    }

    public void Shoot(Vector3 _dir)
    {
        bulletScript.InstatiateBullet(weaponSprite.transform.position, weaponObj.transform.rotation, _dir * shotSpeed );
    }

    
}
