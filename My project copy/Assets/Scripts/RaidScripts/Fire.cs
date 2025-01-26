using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Fire variables
    public float fireSpeed;
    public float damage = 1;
    public float impact = 30;
    public float fireRate = 1;
    float nextTimeToFire = 0;
    public bool automatic = false;
    public float bombCount = 3;
    public float hostageDistance;
    public float hostageGrabRange = 4;


    //Arrest variables
    public float arrestCooldown = 5;
    float distance;
    public float arrestTime;
    public bool frozen;


    //Fire gameobjects
    public GameObject bullet;
    public GameObject player;
    public GameObject hitEffect;
    public GameObject enemyObj;
    public GameObject bomb;
    public GameObject brokenWindow;
    public GameObject hostage;


    //Rigidbodies
    Rigidbody bulletRb;
    Rigidbody enemyRb;
    public Camera cam;

    //Animation
    public ParticleSystem pistolShot;
    public ParticleSystem rifleShot;
    public ParticleSystem shotgunShot;
    public ParticleSystem gunshot;


    //Keys
    public KeyCode fire = KeyCode.Mouse1;


    // Loadout
    public static bool loadout1;
    public static bool loadout2;
    public static bool loadout3;
    public GameObject rifle;
    public GameObject pistol;
    public GameObject shotgun;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodies
        bulletRb = bullet.GetComponent<Rigidbody>();
        enemyRb = enemyObj.GetComponent<Rigidbody>();

        //Camera
        Camera cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //Loadout Stats
            impact = 20;
            fireRate = 1.25f;
            damage = 1;
            automatic = false;

            //Weapon Select
            pistol.active = true;
            rifle.active = false;
            shotgun.active = false;
            gunshot = pistolShot;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            //Loadout Stats
            impact = 15;
            fireRate = 10;
            damage = 0.5f;
            automatic = true;

            //Weapon Select
            pistol.active = false;
            rifle.active = true;
            shotgun.active = false;
            gunshot = rifleShot;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            //Loadout Stats
            impact = 35;
            fireRate = 0.75f;
            damage = 3;
            automatic = false;

            ///Weapon Select
            pistol.active = false;
            rifle.active = false;
            shotgun.active = true;
            gunshot = shotgunShot;
        }


        //Semi Automatic
        if (Input.GetKeyDown(fire) && Time.time >= nextTimeToFire && automatic == false)
        {
            nextTimeToFire = Time.time + 1 / fireRate; 
            shoot();
        }
        //Automatic
        else if (Input.GetKey(fire) && automatic == true && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            shoot();
        }
        //Freeze Enemy
        if (Input.GetKey(KeyCode.Space))
        {
            Freeze();
        }
        else
        {
            ArrestTimer();
        }
        //Handcuff
        if (Input.GetKey(KeyCode.F))
        {
            Handcuff();
            arrestTime += Time.deltaTime;
        }
        //Throw C4
        if (Input.GetKeyDown(KeyCode.Q) && bombCount > 0)
        {
            C4();
        }

    }
    //Fire Weapon
    void shoot()
    {
        gunshot.Play();
        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();

            if (hit.transform.tag == "Enemy")
            {
                hit.rigidbody.AddForce(-hit.normal * impact);
                enemy.TakeDamage(damage);
                Debug.Log("Hit");
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impact);
            }

            if (hit.transform.tag == "Door")
            {
                hit.rigidbody.constraints = RigidbodyConstraints.None;
                hit.rigidbody.AddForce(-hit.normal * impact * 10);
                Debug.Log("Hit Door");
            }

            if (hit.transform.tag == "Window")
            {
                Instantiate(brokenWindow, hit.transform.position, hit.transform.rotation);
                hit.rigidbody.AddForce(-hit.normal * impact * 10);
                Destroy(hit.transform.gameObject);
            }
           
        }
    }

    void Freeze()
    {
        RaycastHit arrest;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out arrest))
        {
            EnemyScript enemy = arrest.transform.GetComponent<EnemyScript>();

            if (arrest.transform.tag == "Enemy")
            {
                Debug.Log("Freeze");
                enemy.frozen = true;
                frozen = true;
                enemyRb.constraints = RigidbodyConstraints.FreezePosition;
                enemy.walkpoint = enemy.transform.position;
                enemy.agent.speed = 0;
                arrestCooldown = 8;
                enemy.attackRange = 0;
                enemy.freezeCooldown = 10;
            }
            if (arrest.transform.tag == "Hostage")
            {
                Debug.Log("Hostage Grabbed");
                GrabHostage();
            }
        }
    }

    void ArrestTimer()
    {
        EnemyScript enemy = enemyObj.GetComponent<EnemyScript>();
        arrestCooldown -= 2 * Time.deltaTime;
        if (arrestCooldown <= 0)
        {
            enemyRb.constraints = RigidbodyConstraints.None;
            enemy.attackRange = 20;
            frozen = false;
            
        }
    }

    void Handcuff()
    {
        distance = Vector3.Distance(transform.position, enemyObj.transform.position);
        EnemyScript enemy = enemyObj.GetComponent<EnemyScript>();
        if (arrestCooldown > 0 && distance < 2 && arrestTime > 3)
        {
            enemy.arrested = true;
        }
    }
    void C4()
    {
        Rigidbody rb = Instantiate(bomb, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z + 0.2f), Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30, ForceMode.Impulse);
        rb.AddForce(transform.up * 3, ForceMode.Impulse);
        bombCount -= 1;
    }
    void GrabHostage()
    {
        GrabHostage hostageRb = hostage.GetComponent<GrabHostage>();
        hostageRb.grabbed = true;

    }
}
