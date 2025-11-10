using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("===Tweaking them Please!!!!==============================================================================================")]
    [SerializeField] float _speed;
    [SerializeField] float _originalSpeed;
    [SerializeField] float _mouse_Camera_Sensitivity;

    [Header("Assign Values")]
    [SerializeField] Rigidbody _rb;
    float _y_Input, _x_Input, _mouseX, _mouseY;
    public bool PickItem;
    public bool IsTop;
    [SerializeField] bool _isCrouch;
    [SerializeField] GameObject _p_Mesh;
    [SerializeField] GameObject _pSight;
    [SerializeField] GameObject _pSight_Anchor;
    Vector3 _dir, _crouchSize, _originalSize;
    [SerializeField] CinemachineVirtualCamera _cam_TakePhoto;
    [SerializeField] Transform _cam;
    [SerializeField] PlayerManager _p_M;


    private void Awake()
    {
        _cam = Camera.main.transform;
        Cursor.lockState= CursorLockMode.Locked;    
        _originalSpeed = _speed;
        _crouchSize = new Vector3(_p_Mesh.transform.localScale.x, _p_Mesh.transform.localScale.y / 2, _p_Mesh.transform.localScale.z);
        _originalSize = _p_Mesh.transform.localScale;
    }
    private void Update()
    {
        CrouchVisual();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovementLogic();
    }
    private void LateUpdate()
    {
    }

    void PlayerMovementLogic()
    {
        //_rb.velocity = transform.forward* _y_Input * _speed+transform.right*_x_Input* _speed;
        switch (_p_M.CurrentState)
        {
            case PlayerManager.PlayerState.GeneralMoving:
                HorizontalMovement();
                break;
            case PlayerManager.PlayerState.CameraShot:
                _rb.velocity = Vector3.zero;
                break;

        }
    }

    void HorizontalMovement()
    {
        //_rb.velocity = new Vector3(_x_Input * _speed, 0, _y_Input * _speed);
        //transform.forward = _dir;//keep the same direction when stop
        // Movement direction based on player’s facing direction
        Vector3 camForward = _cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = _cam.right;
        camRight.y = 0;
        camRight.Normalize();

        // ✅ Combine input with camera orientation
        Vector3 moveDir = camForward * _y_Input + camRight * _x_Input;

        // ✅ Apply movement
        Vector3 targetVelocity = moveDir * _speed;
        targetVelocity.y = _rb.velocity.y; // keep gravity
        _rb.velocity = targetVelocity;

        // ✅ Rotate player to face camera direction when moving
        if (moveDir.sqrMagnitude > 0.01f)
        {
            float rotationSpeed = 10;
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }


    void CrouchVisual()
    {
       Vector3 _originalPos_Sight = _pSight_Anchor.transform.position;
        _p_Mesh.transform.localScale= _isCrouch ? _crouchSize : _originalSize; //if iscrouch is true then first result, if courch is false then second result
        _pSight.transform.position = _isCrouch ? transform.position : _originalPos_Sight;
    }

    public void HorizontalMovement_Input(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            _x_Input = callback.ReadValue<Vector2>().x;
            _y_Input = callback.ReadValue<Vector2>().y;
           // _dir = new Vector3(_x_Input, 0, _y_Input).normalized;
        }
        if (callback.canceled)
        {
            //_dir = new Vector3(_x_Input, 0, _y_Input).normalized;
            _x_Input = 0;
            _y_Input = 0;
        }
    }
    public void DraggingItem(InputAction.CallbackContext callback)
    {
        if (_p_M.CurrentState != PlayerManager.PlayerState.CameraShot)
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

    }

    public void StealthyMode(InputAction.CallbackContext callback)
    {

        if (callback.performed)
        {
            _isCrouch = !_isCrouch;
            if (_isCrouch) _speed /= 2;
            else _speed = _originalSpeed;

        }

    }
    public void Mouse_PosInput(InputAction.CallbackContext callback)
    {
        if (_p_M.CurrentState == PlayerManager.PlayerState.CameraShot)
        {
  
            Vector2 mousePos = callback.ReadValue<Vector2>();

            _mouseX += mousePos.x * _mouse_Camera_Sensitivity * Time.deltaTime;
            _mouseY -= mousePos.y * _mouse_Camera_Sensitivity * Time.deltaTime;

            // Clamp the pitch (X rotation)
            _mouseY = Mathf.Clamp(_mouseY, -80, 80);
            transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
            _cam_TakePhoto.transform.localRotation = Quaternion.Euler(_mouseY, 0f, 0f);
        }


    }
}
