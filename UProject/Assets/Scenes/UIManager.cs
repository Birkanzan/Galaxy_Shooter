using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image LivesImages;
    [SerializeField]
    private Sprite[] livesSprites;
    [SerializeField]
    private Text GameOverText;
    [SerializeField]
    private Text RestartText;

    public int bestScore;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: " + 0;
        GameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        ScoreText.text = "Score: " + playerScore.ToString();
    }


    public void UpdateLives(int CurrentLives)
    {
        LivesImages.sprite = livesSprites[CurrentLives];
        if(CurrentLives == 0)
        {
            GameOverSequince();
        }
    }

    void GameOverSequince()
    {
        _gameManager.GameOver();
        GameOverText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            GameOverText.text = "";
            yield return new WaitForSeconds(0.5f); 
        }
    }
    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu"); 
    }
}
