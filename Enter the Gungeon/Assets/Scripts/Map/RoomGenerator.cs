using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private RoomInstances roomInstances;

    private int rand;
    private bool spawned = false;
    public uint spawnDirection; 
    // 1 -> Left
    // 2 -> Top
    // 3 -> Down
    // 4 -> Right


    // Start is called before the first frame update
    void Start()
    {
        roomInstances = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomInstances>();
        Invoke("SpawnRooms", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void SpawnRooms()
    {
        if (spawned == false)
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
        }
        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomGenerator>().spawned == true)
        {
            //Debug.Log("Will Destroy Room");
            Destroy(gameObject);
        }
    }
}
