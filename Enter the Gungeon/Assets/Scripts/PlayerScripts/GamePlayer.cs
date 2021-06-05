using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    //Scripts I need
    public PlayerController playerControl;

    [SerializeField] private int health = 6;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && playerControl.invincible == false)
        {
            GameBullet bullet = collision.gameObject.GetComponent<GameBullet>();
            health -= bullet.Damage;
            Debug.Log("health = " + health);
            Destroy(bullet.gameObject);
        }
    }
}
