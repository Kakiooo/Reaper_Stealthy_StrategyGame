using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed, _originalSpeed,_mouseBuffer;
    float _y_Input, _x_Input,_mouseX,_mouseY;
    public bool PickItem, IsTop;
    Vector3 _dir;
    [SerializeField] CinemachineVirtualCamera _cam_TakePhoto;
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
    private void LateUpdate()
    {
    }

    void HorizontalMovement()
    {
        //_rb.velocity = transform.forward* _y_Input * _speed+transform.right*_x_Input* _speed;
        switch (_p_M.CurrentState)
        {
            case PlayerManager.PlayerState.GeneralMoving:
                _rb.velocity =new Vector3(_x_Input * _speed, 0, _y_Input * _speed);
                transform.forward = _dir;//keep the same direction when stop
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
            _dir = new Vector3(_x_Input, 0, _y_Input).normalized;
        }
        if (callback.canceled)
        {
            _dir = new Vector3(_x_Input, 0, _y_Input).normalized;
            print(_dir);
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

   public void Mouse_PosInput(InputAction.CallbackContext callback)
    {
        if (_p_M.CurrentState == PlayerManager.PlayerState.CameraShot)
        {
            Vector2 mousePos = callback.ReadValue<Vector2>();

            _mouseX += mousePos.x * _mouseBuffer * Time.deltaTime;
            _mouseY -= mousePos.y * _mouseBuffer * Time.deltaTime;

            // Clamp the pitch (X rotation)
            _mouseY = Mathf.Clamp(_mouseY, -80, 80);

            transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
            _cam_TakePhoto.transform.localRotation = Quaternion.Euler(_mouseY, 0f, 0f);
        }


    }
    void TakePicMode_CameraMovement()
    {
        
    }
}
