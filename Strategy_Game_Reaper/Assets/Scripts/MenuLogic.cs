using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public float CountDownRolling;
    public RectTransform IntroPivot;
    public RectTransform Intro_Panel;
    public RectTransform MenuGroup;
    public bool IsOn;
    public bool StartRolling;
    public CinemachineVirtualCamera FirstCM;
    public CinemachineVirtualCamera SecondCM;
    public CinemachineBrain Brain;
    public Animator FridgeAnimator;
    public GameObject SpotLight_Board;
    public RectTransform HidePos;
    Vector2 _formor_IntroPos;
    Vector2 _menuTab_Pos;
    [SerializeField] AudioSource _selectAudio;

    private void Awake()
    {
        _formor_IntroPos = Intro_Panel.anchoredPosition;
        _menuTab_Pos= MenuGroup.anchoredPosition;   
    }

    private void Update()
    {
        CameraRolling();
    }

    public void StartGame()
    {
        if (FirstCM.gameObject != null && FridgeAnimator.gameObject != null)
        {
            FridgeAnimator.SetBool("IsOpen", true);
            FirstCM.enabled = false;
            StartRolling = true;
            MenuGroup.DOAnchorPos(HidePos.anchoredPosition, 0.5f);
            _selectAudio.Play();
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
                CountDownRolling = 0;
                StartRolling = false;
            }
        }
        else if (Brain.IsBlending == false && SecondCM.isActiveAndEnabled == false)
        {
            SpotLight_Board.gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void ShowInstructionTab()
    {
        IsOn = !IsOn;
        if (IsOn) Intro_Panel.DOAnchorPos(IntroPivot.anchoredPosition, 0.5f);
        else Intro_Panel.DOAnchorPos(_formor_IntroPos, 0.5f);
    }

    public void InstructionEnd_SwitchScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void PlayAudio()
    {
        _selectAudio.Play();
    }



}
