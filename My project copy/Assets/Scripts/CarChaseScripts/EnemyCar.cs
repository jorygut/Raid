using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 10;
    public float shootRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move Forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.x < -3)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (transform.position.x > 3)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
