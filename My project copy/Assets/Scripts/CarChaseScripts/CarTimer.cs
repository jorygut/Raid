using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTimer : MonoBehaviour
{
    public Text score;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        score.text = "Score: " + timer;
    }
    void Timer()
    {
        timer += Time.deltaTime;
    }
}
