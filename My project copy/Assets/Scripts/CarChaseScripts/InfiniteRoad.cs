using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject road1;

    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveRoad()
    {
        Instantiate(road1, new Vector3(road1.transform.position.x, road1.transform.position.y, road1.transform.position.z + offset), Quaternion.identity);
        offset += 50;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "EndRoad")
        {
            moveRoad();
        }
    }
}
