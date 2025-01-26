using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDirectionEnemy : MonoBehaviour
{
    public float speed = 5;
    public float randomNum;
    public float winCooldown;
    public bool tackled;

    public GameObject pistol;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (tackled == true)
        {
            PlayerWins();
            if (winCooldown >= 5)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger"); 
        if (other.transform.tag == "DecisionPoint")
        {
            ChangeDirection();
        }
        if (other.transform.tag == "StoppingPoint")
        {
            Stop();
        }
        if (other.transform.tag == "Player")
        {
            FootChaseMovement playerScript = player.GetComponent<FootChaseMovement>();
            if (playerScript.tackled == true)
            {
                playerScript.speed = 0;
                speed = 0;
                Destroy(pistol);
                transform.eulerAngles = new Vector3(90, 0, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
                tackled = true;
            }
        }
    }
    void ChangeDirection()
    {
        randomNum = Random.Range(-1, 2);

        if (randomNum >= 0)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
        }
        else if (randomNum < 0)
        {
            transform.Rotate(Vector3.forward);
        }
        Debug.Log(randomNum);
    }
    void Stop()
    {
        speed = 0;
        transform.Rotate(0, 180, 0);
    }
    void PlayerWins()
    {
        winCooldown += 1 * Time.deltaTime;
    }
}
