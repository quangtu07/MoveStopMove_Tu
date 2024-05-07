using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CanvasWin : UICanvas
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetUp(int score)
    {
        this.scoreText.text = TextUIConstant.TEXT_SCORE + score.ToString();
    }

    public void TryAgainButton()
    {
        Close(0);
        GameManager.Instance.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnRetry();
    }

    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.BackToMainMenu();
    }

    public void NextLevelButton()
    {
        Close(0);
        LevelManager.Instance.BackToMainMenu();
    }
}
