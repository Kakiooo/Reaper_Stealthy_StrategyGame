using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Assign Values")]
    public bool IsCameraShot, IsIdle;
    [SerializeField] CinemachineVirtualCamera _camera_TakePhoto;
    [SerializeField] CinemachineVirtualCamera _camera_General;

   [SerializeField] Transform  _anchor_TakePhoto;
    [SerializeField] Transform _anchor_AnotherAngle;
    public bool LoseLevel;
    public enum PlayerState
    {
        CameraShot,
        GeneralMoving
    }
    public PlayerState CurrentState;
    private void Awake()
    {
        CurrentState = PlayerState.GeneralMoving;
    }
    private void Update()
    {
        StateSwitch();
        CameraControl();
        CameraFollow();
    }
    void StateSwitch()
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
    public void StateSwitch_Input(InputAction.CallbackContext content)
    {
        if (content.performed)
        {
            IsCameraShot = !IsCameraShot;
        }
    }

    void CameraControl()
    {
        if (CurrentState == PlayerState.CameraShot)
        {
            print("Really not Working?");
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
