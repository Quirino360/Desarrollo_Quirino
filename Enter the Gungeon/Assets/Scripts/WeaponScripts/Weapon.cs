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
    protected GameObject weaponSprite;
    protected Sprite sprite;

    protected uint price = 0;

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

    void Start()
    {
        weaponSprite = GameObject.FindWithTag("Weapon");
        sprite = weaponSprite.GetComponent<SpriteRenderer>().sprite;
        Muzzle = transform.Find("Muzzle").gameObject;
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

    public void SetNewWeapon(Weapon _weapon)
    {
        if (_weapon != null)
        {
            this.sprite = _weapon.sprite;
            /*this.bulletScript = _weapon.bulletScript;

            //this.weaponObj = _weapon.weaponObj;

            this.Muzzle = _weapon.Muzzle;
            this.weaponSprite = _weapon.weaponSprite;

            this.weaponName = _weapon.weaponName;
            this.Quality = _weapon.weaponName;
            this.DPS = _weapon.DPS;
            this.magazineSize = _weapon.magazineSize;
            this.AmmoCapacity = _weapon.AmmoCapacity;
            this.Damage = _weapon.Damage;
            this.FireRate = _weapon.FireRate;
            this.shotSpeed = _weapon.shotSpeed;
            this.Range = _weapon.Range;
            this.Force = _weapon.Force;
            this.Spread = _weapon.Spread;/**/

        }
    }
}
