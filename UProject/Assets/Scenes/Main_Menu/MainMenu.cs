using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single player game loading...");
        SceneManager.LoadScene("Single_Player");
    }

    public void LoadCoOpMode()
    {
        Debug.Log("Co-Op game loading...");
        SceneManager.LoadScene("Co-Op_Mode");
    }
}
