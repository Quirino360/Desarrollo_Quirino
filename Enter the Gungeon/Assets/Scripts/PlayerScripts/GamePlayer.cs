using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayer : MonoBehaviour
{
    //Scripts I need
    public PlayerController playerControl;
    private GameObject gameManager;
    private PauseMenu GMpauseMenu;
    bool hited = false;
    float cooldown = 0;
    [SerializeField] private int health = 6;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GMpauseMenu = gameManager.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if (health <= 0)
        {
            SceneManager.LoadScene("Base");
        }
        if (hited == true && cooldown > 0.25)
        {
            playerControl.CubeRenderer.material.SetColor("_Color", Color.white);
            hited = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            playerControl.CubeRenderer.material.SetColor("_Color", Color.blue);
            Destroy(collision.gameObject);
            health--;
            hited = true;
            cooldown = 0.0f;

            //Debug.Log("Player hited = " + health);
        }
    }
}
