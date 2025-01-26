using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHostage : MonoBehaviour
{
    public bool grabbed;
    public bool safe;
    public float distance = 2;
    public float carDistance;

    public GameObject player;
    public GameObject car;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carDistance = Vector3.Distance(car.transform.position, transform.position);
        if (grabbed == true)
        {
            transform.position = new Vector3(player.transform.position.x - distance, player.transform.position.y + 1, player.transform.position.z);
        }

        if (carDistance <= 5)
        {
            safe = true;
        }
    }
}
