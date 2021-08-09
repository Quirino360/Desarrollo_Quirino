using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    GameObject[] storeWeapons;
    StoreItem[] items;


    Weapon[] weaponsToBuy;

    bool storeIsEmpty;

    // Start is called before the first frame update
    void Start()
    {
        storeWeapons = GameObject.FindGameObjectsWithTag("StoreWeapons");
        /*for (uint i = 0; i < storeWeapons.Length; i++)
        {
            items[i] = storeWeapons[i].GetComponent<StoreItem>();
        }/**/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Base");
        }
    }
}
