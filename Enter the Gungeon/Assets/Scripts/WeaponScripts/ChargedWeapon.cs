using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWeapon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot(Vector3 _dir)
    {
        bulletScript.InstatiateBullet(Muzzle.transform.position, weaponObj.transform.rotation, _dir * shotSpeed);
    }
}