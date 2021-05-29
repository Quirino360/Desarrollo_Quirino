using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase abastracta
public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
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

    public GameObject weapon;
    public GameObject bullet;

    private Vector2 direction;
    void Start()
    {
    }

    
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject bulletCopy = Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
    }

    
}
