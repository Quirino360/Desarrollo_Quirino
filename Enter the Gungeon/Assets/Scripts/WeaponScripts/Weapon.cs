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
    protected GameObject Muzzle;
    private GameObject weaponSprite;

    private uint price = 0;

    // Weapon In Game Variables
    [SerializeField] protected string weaponName = "Default";
    [SerializeField] protected string Quality = "D";
    [SerializeField] protected float DPS = 10.0f;
    [SerializeField] protected int magazineSize = 10;
    [SerializeField] protected int AmmoCapacity = 10;
    [SerializeField] protected float Damage = 10.0f;
    [SerializeField] protected float FireRate = 10.0f;
    [SerializeField] protected float shotSpeed = 10.0f;
    [SerializeField] protected int Range = 10;
    [SerializeField] protected int Force = 10;
    [SerializeField] protected int Spread = 10;

    // Variables


    void Start()
    {
        weaponSprite = GameObject.FindWithTag("WeaponSprite");
        Muzzle = transform.Find("P_Muzzle").gameObject;

    }

    
    void Update()
    {
        
    }

    public virtual void ChangeWeaponRotation(Vector3 _dir, float _angle)
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

    public virtual void Shoot(Vector3 _dir)
    {
        bulletScript.InstatiateBullet(Muzzle.transform.position, weaponObj.transform.rotation, _dir * shotSpeed );
    }

    
}
