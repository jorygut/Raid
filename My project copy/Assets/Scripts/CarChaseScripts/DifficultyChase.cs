using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyChase : MonoBehaviour
{
    public bool easy;
    public bool medium;
    public bool hard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Easy()
    {
        easy = true;
        medium = false;
        hard = false;
        SceneManager.LoadScene("CarChase");
    }
    public void Medium()
    {
        easy = false;
        medium = true;
        hard = false;
        SceneManager.LoadScene("CarChase");
    }
    public void Hard()
    {
        easy = false;
        medium = false;
        hard = true;
        SceneManager.LoadScene("CarChase");
    }
}
