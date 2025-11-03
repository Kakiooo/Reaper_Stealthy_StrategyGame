using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed, _originalSpeed;
    float _y_Input, _x_Input;
    public bool PickItem, IsTop, IsCameraShot;

    [SerializeField] PlayerManager _p_M;


    private void Awake()
    {
        _originalSpeed = _speed;
    }
    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HorizontalMovement();
    }

    void HorizontalMovement()
    {
        //_rb.velocity = transform.forward* _y_Input * _speed+transform.right*_x_Input* _speed;
        switch (_p_M.CurrentState)
        {
            case PlayerManager.PlayerState.GeneralMoving:
                _rb.velocity = new Vector3(_x_Input * _speed, 0, _y_Input * _speed);
                transform.forward = new Vector3(_x_Input, 0, _y_Input);
                break;
            case PlayerManager.PlayerState.CameraShot:
                _rb.velocity = Vector3.zero;
                break;

        }
    }

    public void HorizontalMovement_Input(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            _x_Input = callback.ReadValue<Vector2>().x;
            _y_Input = callback.ReadValue<Vector2>().y;
        }
        if (callback.canceled)
        {
            _x_Input = 0;
            _y_Input = 0;
        }
    }
    public void DraggingItem(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
            }
            PickItem = true;
            _speed /= 2;
        }
        else if (callback.canceled)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(0, 0);
            }
            PickItem = false;
            _speed = _originalSpeed;
        }
    }

    void Mouse_PosInput(InputAction.CallbackContext callback)
    {
        Vector2 mousePos = callback.ReadValue<Vector2>();

    }

    void TakePicMode_CameraMovement()
    {
        
    }
}
