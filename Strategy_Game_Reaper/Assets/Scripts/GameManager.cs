using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
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

    }
    public GameState CurrentState;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        LoseResult();
        EndGamePhase();
        MissionTimer();
        GameState_Determine();
    }

    void GameState_Determine()
    {
        string sceneName=SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Menu":
                CurrentState = GameState.StartPhase;
                break;
            case "LevelSelect":
                CurrentState = GameState.StartPhase;
                break;
            case "CutScene_Story":
                CurrentState = GameState.StartPhase;
                break;
            case "Instruction":
                CurrentState = GameState.StartPhase;
                break;
            case "InGame":
                EndGamePhase();
                    break;
        }
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

    void EndGamePhase()
    {
        if (DisplayResult == null) return;
        if (!DisplayResult.ShownResultEnd_Menu) CurrentState = GameState.InGame;
        else
        {
            PlayerManager.CurrentState = PlayerManager.PlayerState.EndGame;
            CurrentState = GameState.EndPhase;
        }
    }

    
}
