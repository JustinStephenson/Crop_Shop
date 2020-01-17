using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverObject;
    private Image gameOverBackground;
    private static Text winOrLoseText;

    [SerializeField]
    private GameObject nextDayButton;
    [SerializeField]
    private GameObject tryAgainButton;
    [SerializeField]
    private GameObject mainMenuButton;


    private static bool win = false;

    private Color myColor;
    private float alpha;
    private static bool LerpColor = false;
    private const float LERP_SPEED = 0.0075f;
    private float LerpPercentage = 0;

    private void Start()
    {
        gameOverBackground = gameOverObject.GetComponent<Image>();
        winOrLoseText = gameOverObject.GetComponentInChildren<Text>();
        winOrLoseText.enabled = false;
        gameOverBackground.enabled = false;
        myColor = gameOverBackground.color;

        //GameOver(true);
    }

    private void Update()
    {
        if (LerpColor)
        {
            if (!gameOverObject.activeSelf)
            {
                gameOverObject.SetActive(true);
            }
            gameOverBackground.enabled = true;
            LerpPercentage += LERP_SPEED;
            alpha = Mathf.Lerp(0.0f, 1.0f, LerpPercentage);
            myColor.a = alpha;
            gameOverBackground.color = myColor;
            if (LerpPercentage >= 1)
            {
                myColor.a = 1.0f;
                gameOverBackground.color = myColor;
                LerpColor = false;
                ShowGameOver();
            }
        }
    }

    public static void GameOver(bool win)
    {
        LerpColor = true;
        ClickTile.pause = true;
        GameOverManager.win = win;
        //Time.timeScale = 0;
        if (win)
        {
            Debug.Log("You Win!!!");
            winOrLoseText.text = "You Win!";
        }
        else
        {
            Debug.Log("You Lose!!!");
            winOrLoseText.text = "You Lose!";
        }
    }

    private void ShowGameOver()
    {
        winOrLoseText.enabled = true;
        if (win)
        {
            nextDayButton.SetActive(true);
        }
        else
        {
            tryAgainButton.SetActive(true);
        }
        mainMenuButton.SetActive(true);
    }
}
