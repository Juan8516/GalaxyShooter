using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Lives
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;

    public TextMeshProUGUI scoreText;
    public int scorePlayer;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("The lives current" + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        scorePlayer += 10;
        scoreText.text = "Score: " + scorePlayer;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen() 
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }
}
