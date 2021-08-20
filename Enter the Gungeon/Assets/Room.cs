using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject enemyInstance;

    [SerializeField] private bool enemiesSpawned = false;
    private bool roomSpawned = true;


    void Start()
    {
        //enemyInstance = GameObject.FindGameObjectWithTag("Enemy");
        //C:\Users\USER\Documents\GitHubs\Desarrollo_Quirino\Enter the Gungeon\Assets\Prefabs\Enemies
        enemyInstance = Resources.Load("Prefabs/Enemies/Enemy", typeof(GameObject)) as GameObject;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player") && enemiesSpawned == false && WaitUntil.Equals(roomSpawned, true))
        {
            int range = Random.Range(1, 4);
            Debug.Log("Player Is Inside - Range = " + range);

            for (int i = 0; i < range; i++)
            {
                Instantiate(enemyInstance, this.transform.position + new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.3f, 3.3f), 0), this.transform.rotation);
            }

            enemiesSpawned = true;
        }/**/
    }
}
