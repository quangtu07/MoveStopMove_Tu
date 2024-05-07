using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLose : UICanvas
{
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

}
