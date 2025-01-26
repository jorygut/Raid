using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Game Objects
    public GameObject hostage;
    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GrabHostage hostageScript = hostage.GetComponent<GrabHostage>();
        PlayerController playerControl = player.GetComponent<PlayerController>();
        if (hostageScript.safe == true)
        {
            SceneManager.LoadScene("Win");
        }
        if (playerControl.health <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
