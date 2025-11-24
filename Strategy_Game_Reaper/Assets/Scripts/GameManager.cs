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
    public float CountDownRolling;
    public PlayerManager PlayerManager;
    public Ending_DisplayResult DisplayResult;
    public RectTransform IntroPivot;
    public RectTransform Intro_Panel;
    public bool IsOn;
    public bool StartRolling;
    public CinemachineVirtualCamera FirstCM;
    public CinemachineVirtualCamera SecondCM;
    public Animator FridgeAnimator;
    public GameObject SpotLight_Board;
    Vector2 _formor_IntroPos;

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
        _formor_IntroPos = Intro_Panel.anchoredPosition;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        LoseResult();
        EndGamePhase();
        MissionTimer();
        GameState_Determine();
        CameraRolling();
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


    public void StartGame()
    {
        if (FirstCM.gameObject != null && FridgeAnimator.gameObject != null)
        {
            FridgeAnimator.SetBool("IsOpen", true);
            FirstCM.enabled = false;
            StartRolling=true;
        }

    }
    public void CameraRolling()
    {
        if (StartRolling)
        {
            CountDownRolling += Time.deltaTime;
            if (CountDownRolling >= 3)
            {
                SecondCM.enabled = false;
                SpotLight_Board.gameObject.SetActive(true);
                CountDownRolling = 0;
                StartRolling =false;
            }
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowInstructionTab()
    {
        IsOn = !IsOn;
        if(IsOn) Intro_Panel.DOAnchorPos(IntroPivot.anchoredPosition, 0.5f);
        else Intro_Panel.DOAnchorPos(_formor_IntroPos, 0.5f);
    }

    public void InstructionEnd_SwitchScene()
    {
        SceneManager.LoadScene("InGame");
    }

    
}
