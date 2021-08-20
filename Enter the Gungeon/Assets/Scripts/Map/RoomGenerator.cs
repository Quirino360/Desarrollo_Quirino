using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private RoomInstances roomInstances;
    private GameObject bossInstance;


    private int rand;
    public uint spawnDirection; 
    private bool roomSpawned = false;
    [SerializeField] private bool enemiesSpawned = false;

    private bool spawnBoss = false;

    // 1 -> Left
    // 2 -> Top
    // 3 -> Down
    // 4 -> Right


    // Start is called before the first frame update
    void Start()
    {
        //enemyInstance = GameObject.FindGameObjectWithTag("Enemy");
        //C:\Users\USER\Documents\GitHubs\Desarrollo_Quirino\Enter the Gungeon\Assets\Prefabs\Enemies
        bossInstance = Resources.Load("Prefabs/Enemies/Boss", typeof(GameObject)) as GameObject;
        roomInstances = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomInstances>();
        Invoke("SpawnRooms", 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void SpawnRooms()
    {
        if (roomSpawned == false)
        {
            if (spawnDirection == 1) // Left
            {
                rand = Random.Range(0, roomInstances.leftRooms.Length);
                Instantiate(roomInstances.leftRooms[rand], transform.position, roomInstances.leftRooms[rand].transform.rotation);
            }
            else if (spawnDirection == 2) //Top
            {
                rand = Random.Range(0, roomInstances.topRooms.Length);
                Instantiate(roomInstances.topRooms[rand], transform.position, roomInstances.topRooms[rand].transform.rotation);
            }
            else if (spawnDirection == 3) //Down
            {
                rand = Random.Range(0, roomInstances.downRooms.Length);
                Instantiate(roomInstances.downRooms[rand], transform.position, roomInstances.downRooms[rand].transform.rotation);
            }
            else if (spawnDirection == 4) //Right
            {
                rand = Random.Range(0, roomInstances.rightRooms.Length);
                Instantiate(roomInstances.rightRooms[rand], transform.position, roomInstances.rightRooms[rand].transform.rotation);
            }
            else
            {
                spawnBoss = true;
            }
        }
        roomSpawned = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomGenerator>().roomSpawned == true)
        {
            //Debug.Log("Will Destroy Room");
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && spawnBoss == true && WaitUntil.Equals(roomSpawned, true))
        {
            int range = Random.Range(0, 4);
            Debug.Log("Player Is Inside - Range = " + range);
            
            Instantiate(bossInstance, this.transform.position + new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.3f, 3.3f), 0), this.transform.rotation);
            spawnBoss = false;
        }/**/
    }
}
