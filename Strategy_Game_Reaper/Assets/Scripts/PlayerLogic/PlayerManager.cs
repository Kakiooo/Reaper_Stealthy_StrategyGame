using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public ColorGrading G_ColorGrading;
    public LensDistortion L_D;
    [SerializeField] float _depth,_distort,_saturation,_lens;

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
        PosProcess.profile.TryGetSettings(out G_ColorGrading);
        PosProcess.profile.TryGetSettings(out L_D);
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
            _depth += Time.deltaTime*2;
            _distort += Time.deltaTime*5;
            _saturation += Time.deltaTime * 40;
            _lens -= Time.deltaTime * 20;

            _saturation = Mathf.Clamp(_saturation,0, 100);
            _distort = Mathf.Clamp(_distort, 0, 1);
            _lens = Mathf.Clamp(_lens, -30, 0);

            FloatParameter newFocusDis = new FloatParameter { value = _depth };
            DF.enabled.value = true;
            DF.focusDistance.value = newFocusDis;

            SetValueForPostProcessint();

            //if (_lens >= 50) _lens = 50;
            //if (_distort >= 1) _distort = 1;
            if (_depth >= 5)
            {
                _distort = 0;
                _saturation = 0;
                _depth = 0;
                _lens = 0;
                _playBlury = false;
            }
        }
     


    }

    void SetValueForPostProcessint()
    {


        FloatParameter newDistort = new FloatParameter { value = _distort };
        CA.enabled.value = true;
        CA.intensity.value = newDistort;

        FloatParameter newSaturation = new FloatParameter { value = _saturation };
        G_ColorGrading.enabled.value = true;
        G_ColorGrading.saturation.value = newSaturation;

        FloatParameter newLens = new FloatParameter { value = _lens };
        L_D.enabled.value = true;
        L_D.intensity.value = newLens;
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
