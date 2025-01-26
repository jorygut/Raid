using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleController : MonoBehaviour
{
    public float turnSpeed = 25;
    public float acceleration = 10;
    public float reverseSpeed = 3;
    public float timeUntilDeath = 5;
    public bool crashed;

    public ParticleSystem explosion;
    public ParticleSystem blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(Vector3.back * Time.deltaTime * reverseSpeed);
        }
        if (crashed == true)
        {
            DeathDelay();
            if (timeUntilDeath >= 15)
            {
                SceneManager.LoadScene("Lose");
            }
        }

    }
    void DeathDelay()
    {
        timeUntilDeath += 1 * Time.deltaTime;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall" || collision.transform.tag == "CivillianCar")
        {
            explosion.Play();
            crashed = true;
            acceleration = 0;
            turnSpeed = 0;
            reverseSpeed = 0;
        }
        if (collision.transform.tag == "Bullet")
        {
            blood.Play();
            crashed = true;
            acceleration = 0;
            turnSpeed = 0;
            reverseSpeed = 0;
        }
    }
}
