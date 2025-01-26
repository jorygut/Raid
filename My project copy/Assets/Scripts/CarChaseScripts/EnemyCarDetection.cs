using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarDetection : MonoBehaviour
{
    public float evasionSpeed;

    public GameObject enemyCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detect incoming cars
        RaycastHit carDetect;
        if (Physics.Raycast(transform.position, Vector3.forward, out carDetect))
        {
            if (carDetect.transform.tag == "CivillianCar" && transform.position.x < 3)
            {
                Debug.Log("Car in front");
                enemyCar.transform.Translate(Vector3.left * Time.deltaTime * evasionSpeed);
            }
            else if (carDetect.transform.tag == "CivillianCar" && transform.position.x < -1.5)
            {
                Debug.Log("Car in front");
                enemyCar.transform.Translate(Vector3.right * Time.deltaTime * evasionSpeed);
            }
        }
    }
}
