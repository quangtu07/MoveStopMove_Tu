using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        ChangeState(GameState.MainMenu);
        LoadUserData.Instance.LoadData();
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }

    private void OnApplicationQuit()
    {
        LoadUserData.Instance.SaveData();
    }

}

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Pause,
    Lose,
    ShopWeapon,
    ShopPant,
    ShopHat
}
