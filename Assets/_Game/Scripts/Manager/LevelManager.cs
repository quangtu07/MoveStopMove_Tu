using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private DynamicJoystick joystickPrefab;
    [SerializeField] private Canvas joystickHolder;
    [SerializeField] private List<Level> listLevel;
    public int currentLevelIndex;
    private Level currentLevel;
    private Player currentPlayer;
    private DynamicJoystick currentJoystick;
    private int defaultValue = 0;

    public Level CurrentLevel { get => currentLevel; }

    public void OnInit()
    {
        if (LoadUserData.Instance.data != null)
        {
            currentLevelIndex = LoadUserData.Instance.data.currentLevel;
        } else
        {
            currentLevelIndex = 1;
        }
        LoadLevel();
        LoadCharacter();
    }

    public void LoadCharacter()
    {
        CleanupPlayerAndJoystick();  // Cleanup before loading new character

        currentPlayer = LeanPool.Spawn(playerPrefab, CurrentLevel.playerStartPoint.position, Quaternion.identity);
        if (LoadUserData.Instance.data != null)
        {
            int weaponTypeIndex = LoadUserData.Instance.data.currentWeapon;
            int hatTypeIndex = LoadUserData.Instance.data.currentHat;
            int pantTypeIndex = LoadUserData.Instance.data.currentPant;
            currentPlayer.OnInit(weaponTypeIndex, hatTypeIndex, pantTypeIndex);
        }
        else
        {
            currentPlayer.OnInit(defaultValue, defaultValue, defaultValue);
        }
        // Spawn the joystick and assign it to the player
        currentJoystick = LeanPool.Spawn(joystickPrefab);

        currentJoystick.transform.SetParent(joystickHolder.transform, false);
        currentJoystick.transform.position = Vector3.zero;
        currentJoystick.transform.rotation = Quaternion.identity;


        currentPlayer.joystick = currentJoystick;

        // Inform the CameraFollow to start following the newly spawned player
        if (CameraFollow.Instance != null)
        {
            CameraFollow.Instance.SetPlayer(currentPlayer);
        }

    }

    private void CleanupPlayerAndJoystick()
    {

        if (currentPlayer != null)
        {
            if (currentPlayer.joystick != null)
            {
                LeanPool.Despawn(currentPlayer.joystick);
                currentPlayer.joystick = null;
            }
            LeanPool.Despawn(currentPlayer);
            currentPlayer = null;
        }

        if (CameraFollow.Instance != null)
        {
            CameraFollow.Instance.RemovePlayer();  // Also detach the camera here
        }
    }

    public void LoadLevel()
    {
        LoadLevel(currentLevelIndex);
        //UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>().SetUp();
    }

    public void LoadLevel(int currentIndexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            //LeanPool.Despawn(currentLevel);
        }
        if (currentIndexLevel <= listLevel.Count)
        {
            currentLevel = Instantiate(listLevel[currentIndexLevel - 1]);
            //currentLevel = LeanPool.Spawn(listLevel[currentIndexLevel - 1]);
            currentLevel.OnInit(currentIndexLevel * 20);
        }
    }

    public void OnRetry()
    {
        CleanupPlayerAndJoystick();
        LoadLevel();
        LoadCharacter();
    }

    public void BackToMainMenu()
    {
        CleanupPlayerAndJoystick();
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
    }

    public void NextLevel(int index)
    {
        int currentIndex = index;
        currentIndex++;
        if (currentIndex <= listLevel.Count)
        {
            currentLevelIndex = currentIndex;
            LoadLevel();
            LoadCharacter();
        } else
        {
            UIManager.Instance.OpenUI<CanvasNotAvailable>();
        }
    }

    public void OnLose()
    {
        UIManager.Instance.OpenUI<CanvasLose>();
        GameManager.Instance.ChangeState(GameState.Lose);

        if (CameraFollow.Instance != null)
        {
            CameraFollow.Instance.RemovePlayer();  // Stop the camera from following when the player loses
        }
    }

    public void OnWin(int bestScore)
    {
        UIManager.Instance.OpenUI<CanvasWin>().SetUp(bestScore);
        GameManager.Instance.ChangeState(GameState.Win);
    }
}
