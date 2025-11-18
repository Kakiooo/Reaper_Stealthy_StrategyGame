using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool LoseLevel;
    public float CountDown;
    public PlayerManager PlayerManager;
    public Ending_DisplayResult DisplayResult;
    public enum GameState
    {
        StartPhase,
        InGame,
        EndPhase,
        GameWin,
        GameLose,

    }
    public GameState CurrentState;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LoseResult();
        FreezeAllMovement();
    }

    void LoseResult()
    {
        if (LoseLevel)
        {
            CountDown-=Time.deltaTime;
            if(CountDown <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    void FreezeAllMovement()
    {
        if (DisplayResult.ShownResultEnd_Menu)
        {
            PlayerManager.CurrentState=PlayerManager.PlayerState.EndGame;
        }
    }

}
