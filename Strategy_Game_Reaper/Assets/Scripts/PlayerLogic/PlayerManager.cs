using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class PlayerManager : MonoBehaviour
{
    [Header("Assign Values")]
    public bool IsCameraShot, IsIdle;
    [SerializeField] CinemachineVirtualCamera _camera_TakePhoto;
    [SerializeField] CinemachineVirtualCamera _camera_General;

   [SerializeField] Transform  _anchor_TakePhoto;
    [SerializeField] Transform _anchor_AnotherAngle;
    public bool LoseLevel;
    public GameManager GameManager;
    public CinemachineBrain MainCM;
    public PostProcessVolume PosProcess;
    public DepthOfField DF;
    public ChromaticAberration CA;
    [SerializeField] float _depth,_distort;
    bool _playBlury;
    public enum PlayerState
    {
        CameraShot,
        GeneralMoving,
        EndGame
    }
    public PlayerState CurrentState;
    private void Awake()
    {
        CurrentState = PlayerState.GeneralMoving;
        PosProcess.profile.TryGetSettings(out DF);
        PosProcess.profile.TryGetSettings(out CA);
    }
    private void Update()
    {
       StateSwitch();
        CameraControl();
        CameraFollow();
        SwitchCamera_Blury();
    }

    void SwitchCamera_Blury()
    {
        if (MainCM.IsBlending&& _camera_General.enabled==false)
        {
            //PosProcess.enabled = true;
            _playBlury=true;
        }

        if (_camera_General.enabled)
        {
            _distort = 0;
            FloatParameter newDistort = new FloatParameter { value = _distort };
            CA.enabled.value = true;
            CA.intensity.value = newDistort;
        }

        if (_playBlury)
        {
            _depth += Time.deltaTime*5;
            _distort += Time.deltaTime;

            FloatParameter newFocusDis = new FloatParameter { value = _depth };
            DF.enabled.value = true;
            DF.focusDistance.value = newFocusDis;

            FloatParameter newDistort = new FloatParameter { value = _distort };
            CA.enabled.value = true;
            CA.intensity.value = newDistort;
            if (_distort >= 1) _distort = 1;
            if (_depth >= 5)
            {
                _depth = 0;
                _playBlury = false;
            }


        }


    }


    void StateSwitch()
    {
        if (GameManager.CurrentState!=GameManager.GameState.EndPhase)
        {
            if (IsCameraShot)
            {
                CurrentState = PlayerState.CameraShot;
            }
            else
            {
                CurrentState = PlayerState.GeneralMoving;
            }
        }

    }
    public void StateSwitch_Input(InputAction.CallbackContext content)
    {
        if (content.performed&& MainCM.IsBlending==false)
        {
            IsCameraShot = !IsCameraShot;
        }
    }

    void CameraControl()
    {
        if (CurrentState == PlayerState.CameraShot)
        {
            _camera_General.enabled = false;
        }
        else if (CurrentState == PlayerState.GeneralMoving)
        {
            _camera_General.enabled = true;
        }
    }
    void CameraFollow()
    {
        _camera_General.transform.position = _anchor_AnotherAngle.transform.position;
        _camera_TakePhoto.transform.position = _anchor_TakePhoto.transform.position;
    }


}
