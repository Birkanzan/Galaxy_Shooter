using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]    
    private bool isGameOver;
    public bool isCoOpMode = false;
    private SpawnManager spawnManager;
    [SerializeField]
    private GameObject PauseMenu;

    private Animator PauseAnimator;
    private void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        PauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        PauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(1);
            spawnManager.StartSpawning();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_menu");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu.SetActive(true);
            PauseAnimator.SetBool("ÝsPaused", true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1; 
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
