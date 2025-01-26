using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FootChaseMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public ParticleSystem blood;
    public Rigidbody rb;
    public GameObject playerObj;

    public float speed = 5;
    public float smoothVelocity;
    public float turnSmoothSpeed = 0.1f;
    public float deathTime;
    public bool dead;
    public bool tackled;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hInput, 0, vInput).normalized;

        if (direction.magnitude >= 0.1)
        {
            //Look Direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothSpeed);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            //Move Direction
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (dead == true)
        {
            DeathTimer();
            if (deathTime >= 5)
            {
                SceneManager.LoadScene("Lose");
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Tackle();
        }
    }
    void DeathTimer()
    {
        deathTime += 1 * Time.deltaTime;
    }
    void Tackle()
    {
        if (dead != true)
        {
            transform.Rotate(75, 0, 0);
            rb.AddForce(Vector3.up * 100);
            tackled = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("Dead");
            blood.Play();
            speed = 0;
            rb.isKinematic = false;
            playerObj.transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, transform.position.z - 90);
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - 0.2f, playerObj.transform.position.z);
            dead = true;
        }
    }
}
