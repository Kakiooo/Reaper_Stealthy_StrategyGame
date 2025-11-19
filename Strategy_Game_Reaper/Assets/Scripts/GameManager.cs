using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool LoseLevel;
    public float CountDown;
    public float Timer_Result;
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
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        LoseResult();
        FreezeAllMovement();
        MissionTimer();
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

    void MissionTimer()
    {
        if(CurrentState==GameState.InGame) Timer_Result += Time.deltaTime;//need to be display at the end of game

    }

    void FreezeAllMovement()
    {
        if (DisplayResult == null) return;
        if (DisplayResult.ShownResultEnd_Menu)
        {
            PlayerManager.CurrentState=PlayerManager.PlayerState.EndGame;
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void InstructionEnd_SwitchScene()
    {
        SceneManager.LoadScene(2);
    }

    
}
