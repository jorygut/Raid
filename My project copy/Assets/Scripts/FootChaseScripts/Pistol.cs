using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float shootSpeed;
    public float shootDistance;
    public float cooldown = 5;

    public GameObject player;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        transform.LookAt(player.transform.position);
        if (playerDistance <= shootDistance && cooldown >= shootSpeed)
        {
            Shoot();
            cooldown = 0;
        }
        ShootCooldown();
    }
    void Shoot()
    {
        Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 80, ForceMode.Impulse);
        rb.AddForce(transform.up * 3, ForceMode.Impulse);
    }
    void ShootCooldown()
    {
        if (cooldown < shootSpeed)
        {
            cooldown += 1 * Time.deltaTime;
        }
    }
}
