using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed, _originalSpeed;
    float _y_Input, _x_Input;
    public bool PickItem,IsTop;
    [SerializeField] CinemachineVirtualCamera _camera_AnotherAngle;  
    private void Awake()
    {
        _originalSpeed=_speed;
    }
    private void Update()
    {
        CameraControl();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HorizontalMovement();
    }

    void HorizontalMovement()
    {

        //_rb.velocity = new Vector3(_speed * _x_Input, 0, _y_Input * _speed);
        _rb.velocity = transform.forward* _y_Input * _speed+transform.right*_x_Input* _speed;
    }

    public void HorizontalMovementInput(InputAction.CallbackContext callback)
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

    void CameraControl()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            IsTop=!IsTop;
        }
        if (IsTop)
        {
            _camera_AnotherAngle.transform.gameObject.SetActive(false);
        }
        else
        {
            _camera_AnotherAngle.transform.gameObject.SetActive(true);
        }

    }
}
