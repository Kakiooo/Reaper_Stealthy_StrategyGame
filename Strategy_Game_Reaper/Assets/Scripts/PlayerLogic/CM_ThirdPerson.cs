using UnityEngine;
using UnityEngine.InputSystem;

public class CM_ThirdPerson : MonoBehaviour
{
    [Header("References")]
    public Transform Player; // Player target
    public Transform Cam;    // The virtual camera's Follow target (pivot)
    public PlayerManager P_M;    // The virtual camera's Follow target (pivot)

    [Header("Settings")]
    public float SensitivityX = 180f;
    public float Angle_Down;// Constant downward tilt angle
    public float SmoothTime = 0.1f;

    private float _x_input;
    private float _mouseX;
    private Vector2 _smoothInput;
    private Vector2 _currentVelocity;
    public float RotationDegree;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (P_M.CurrentState == PlayerManager.PlayerState.GeneralMoving)
        {
            _smoothInput = Vector2.SmoothDamp(_smoothInput, new Vector2(_mouseX, 0f), ref _currentVelocity, SmoothTime);

            _x_input += _smoothInput.x * SensitivityX * Time.deltaTime;
            transform.rotation = Quaternion.Euler(Angle_Down, _x_input, 0f);
            transform.position = Player.position;

            if (Cam) Cam.rotation = transform.rotation;
            if (Player) Player.rotation = Quaternion.Euler(0, _x_input, 0);

        }
    }

    public void GetMouseInput(InputAction.CallbackContext ctx)
    {
        _mouseX = ctx.ReadValue<Vector2>().x;
    }
}

