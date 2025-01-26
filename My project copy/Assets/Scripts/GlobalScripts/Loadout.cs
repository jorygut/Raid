using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadout : MonoBehaviour
{
    //Gameobjects
    public GameObject playerObj;

    public static bool loadout1Bool;
    public static bool loadout2Bool;
    public static bool loadout3Bool;



    // Start is called before the first frame update
    void Start()
    {
        Fire fireScript = playerObj.GetComponent<Fire>();
        PlayerController playerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadout1()
    {
        loadout1Bool = true;
        SceneManager.LoadScene("SampleScene");
    }

    public void loadout2()
    {
        SceneManager.LoadScene("SampleScene");
        loadout2Bool = true;
    }

    public void loadout3()
    {
        SceneManager.LoadScene("SampleScene");
        loadout3Bool = true;
    }
    public void CarChase()
    {
        SceneManager.LoadScene("DifficultyChase");
    }
    public void Raid()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("GameSelect");
    }
    public void DifficultyChase()
    {
        SceneManager.LoadScene("DifficultyChase");
    }
}
