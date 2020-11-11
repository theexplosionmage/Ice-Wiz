using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Button playButton;
    public Button controlsButton;
    public Button backButton;
    public Button menuButton;

    public GameObject menuCanvas;
    public GameObject controlsCanvas;
    public GameObject gameOverCanvas;
    public GameObject highScoreTexts;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        controlsButton.onClick.AddListener(OnControlsButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
        menuButton.onClick.AddListener(OnMenuButtonClick);

        if(PlayerPrefs.GetInt("HighScore", 0) != 0)
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
            highScoreTexts.SetActive(true);
        }

        if (StaticVars.GameOver)
        {
            scoreText.text = StaticVars.Score.ToString();
            menuCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
    }

    private void OnPlayButtonClick()
    {
        StaticVars.Score = 0;
        SceneManager.LoadScene(1);
    }

    private void OnControlsButtonClick()
    {
        menuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    private void OnBackButtonClick()
    {      
        controlsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    private void OnMenuButtonClick()
    {
        gameOverCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }




}
