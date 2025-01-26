using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivillianCarSpawner : MonoBehaviour
{
    public float interval;
    float xPos;
    public float spawnCount;

    public GameObject civCar;
    // Start is called before the first frame update
    void Start()
    {
        DifficultySelect();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer();
        if (spawnCount >= interval)
        {
            spawnCar();
            spawnCount = 0;
        }
    }

    void spawnCar()
    {
        xPos = Random.Range(4, -4);
        Instantiate(civCar, new Vector3(xPos, transform.position.y, transform.position.z), Quaternion.identity);
    }
    void spawnTimer()
    {
        spawnCount += 1 * Time.deltaTime;
    }
    void DifficultySelect()
    {
        DifficultyChase difficulty = GetComponent<DifficultyChase>();

        if (difficulty.easy == true)
        {
            interval = 15;
        }
        else if (difficulty.medium == true)
        {
            interval = 10;
        }
        else if (difficulty.hard == true)
        {
            interval = 5;
        }
    }
}
