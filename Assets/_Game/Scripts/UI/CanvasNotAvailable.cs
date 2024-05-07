using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNotAvailable : UICanvas
{
    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasWin>();
        GameManager.Instance.ChangeState(GameState.Win);
    }
}
