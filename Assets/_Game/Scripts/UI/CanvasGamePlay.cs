using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetUp()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        this.scoreText.text = TextUIConstant.TEXT_SCORE + score.ToString();
    }

    public void PauseButton()
    {
        UIManager.Instance.OpenUI<CanvasPause>();
        GameManager.Instance.ChangeState(GameState.Pause);
    }
}
