using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public GameObject house;
    public GameObject hookObj;
    Rigidbody rb;

    public float climbSpeed = 1;
    public float hookDistance;
    public bool hooked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.E) && hooked == false && hookDistance < 15)
        {
            hook();
        }
        else if ((Input.GetKey(KeyCode.E) && hooked == true) || player.transform.position. y > 16)
        {
            hooked = false;
        }

        if (Input.GetKey(KeyCode.Space) && hooked == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            player.transform.Translate(Vector3.up * Time.deltaTime * climbSpeed);
        }
        else if (hooked == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (hooked == false)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    void hook()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit);
        hookDistance = player.transform.position.y - hit.transform.position.y;
        Vector3 hookPos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 2);
        Instantiate(hookObj, hookPos, hit.transform.rotation);
        if (hit.transform.tag == "Climbable")
        {
            hooked = true;
        }

    }
}
