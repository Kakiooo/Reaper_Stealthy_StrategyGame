using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    public Volume PosProcess;
    public DepthOfField DF;
    public ChromaticAberration CA;
    public ColorAdjustments G_ColorGrading;
    public LensDistortion L_D;
    [SerializeField] float _depth,_distort,_saturation,_lens;

    bool _playBlury;
    public bool Spotted;
    public enum PlayerState
    {
        CameraShot,
        GeneralMoving,
        GetSpoted,
        EndGame
    }
    public PlayerState CurrentState;
    private void Awake()
    {
        CurrentState = PlayerState.GeneralMoving;
        PosProcess.profile.TryGet(out DF);
        PosProcess.profile.TryGet(out CA);
        PosProcess.profile.TryGet(out G_ColorGrading);
        PosProcess.profile.TryGet(out L_D);
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

        if (_camera_General.enabled) //reset value when switch camera
        {
            SetValueForPostProcessint();
        }

        if (_playBlury)
        {
            _depth += Time.deltaTime*5;
            _distort += Time.deltaTime*5;
            _saturation += Time.deltaTime * 40;
            _lens -= Time.deltaTime * 20;

            _saturation = Mathf.Clamp(_saturation,0, 100);
            _distort = Mathf.Clamp(_distort, 0, 1);
            _lens = Mathf.Clamp(_lens, -0.4f, 0);

            DF.focusDistance.value = _depth;

            SetValueForPostProcessint();

            //if (_lens >= 50) _lens = 50;
            //if (_distort >= 1) _distort = 1;
            if (_depth >= 15&& MainCM.IsBlending==false)
            {
                _playBlury = false;
                _distort = 0;
                _saturation = 0;
                _depth = 0;
                _lens = 0;

            }
        }
     


    }

    void SetValueForPostProcessint()
    {
        CA.intensity.value = _distort;

        G_ColorGrading.saturation.value = _saturation;

        L_D.intensity.value = _lens;
    }


    void StateSwitch()
    {
        if (GameManager.CurrentState!=GameManager.GameState.EndPhase&&!Spotted)
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
            PosProcess.enabled = true;
        }
        else if (CurrentState == PlayerState.GeneralMoving)
        {
            _camera_General.enabled = true;
            PosProcess.enabled = true;
        }
    }
    void CameraFollow()
    {
        _camera_General.transform.position = _anchor_AnotherAngle.transform.position;
        _camera_TakePhoto.transform.position = _anchor_TakePhoto.transform.position;
    }


}
