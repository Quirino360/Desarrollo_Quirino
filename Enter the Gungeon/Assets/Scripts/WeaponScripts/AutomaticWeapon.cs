using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : Weapon
{


    // Start is called before the first frame update
    void Start()
    {
        weaponSprite = GameObject.FindWithTag("WeaponSprite");
        sprite = weaponSprite.GetComponent<SpriteRenderer>().sprite;
        Muzzle = weaponSprite.transform.Find("Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ChangeWeaponRotation(Vector3 _dir, float _angle)
    {
        //Debug.Log(_dir.y);
        if (_dir.y <= 1.0f && _dir.y >= -1.0f)
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

    public override void Shoot(Vector3 _dir)
    {
        bulletScript.InstatiateBullet(Muzzle.transform.position, weaponObj.transform.rotation, _dir * shotSpeed);
    }
}
