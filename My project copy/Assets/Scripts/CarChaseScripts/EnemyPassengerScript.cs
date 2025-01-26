using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassengerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;

    public ParticleSystem gunshot;

    public float cooldown;
    public float cooldownTime;
    public float startShooting;

    public Vector3 predictionShot;
    // Start is called before the first frame update
    void Start()
    {
        DifficultySelect();
        
    }

    // Update is called once per frame
    void Update()
    {
        predictionShot = new Vector3(player.transform.position.x, player.transform.position.y - 8, player.transform.position.z + 3);
        StartShootingCooldown();
        transform.LookAt(predictionShot);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 15 && startShooting >= 10)
        {
            Shoot();
            ShootCooldown();
        }
    }
    void Shoot()
    {
        if (cooldown >= cooldownTime)
        {
            gunshot.Play();
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 50, ForceMode.Impulse);
            rb.AddForce(transform.up * 1, ForceMode.Impulse);
            cooldown = 0;
        }
    }
    void ShootCooldown()
    {
        cooldown += 1 * Time.deltaTime;
    }
    void StartShootingCooldown()
    {
        startShooting += 1 * Time.deltaTime;
    }
    void DifficultySelect()
    {
        DifficultyChase difficulty = GetComponent<DifficultyChase>();

        if (difficulty.easy == true)
        {
            cooldownTime = 10;
        }
        else if (difficulty.medium == true)
        {
            cooldownTime = 5;
        }
        else if (difficulty.hard == true)
        {
            cooldownTime = 1;
        }
    }
}
