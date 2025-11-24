using UnityEngine;

public class ExitArrowIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;       // Player transform
    [SerializeField] private Transform _exitPoint;    // Exit object (can be null)
    [SerializeField] private Transform _arrow;        // 3D arrow object

    [Header("Settings")]
    [SerializeField] private float _heightAbovePlayer = 2f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private float _followSmooth = 10f;

    private void Update()
    {
        SetExit(_exitPoint.gameObject.activeSelf);
    }

    private void LateUpdate()
    {
        // ------------------------------
        // 0. EXIT CHECK ¡ª Arrow only active if exitPoint exists
        // ------------------------------
        bool exitExists = (_exitPoint != null && _exitPoint.gameObject.activeInHierarchy);

        _arrow.gameObject.SetActive(exitExists);

        if (!exitExists)
            return;  // stop logic if no exit

        Vector3 targetPos = _player.position + Vector3.up * _heightAbovePlayer;
        _arrow.position = Vector3.Lerp(_arrow.position, targetPos, _followSmooth * Time.deltaTime);

        Vector3 dir = _exitPoint.position - _arrow.position;
        dir.y = 0f; // keep horizontal

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            _arrow.rotation = Quaternion.Slerp(_arrow.rotation, targetRot, _rotateSpeed * Time.deltaTime);
        }
    }

    // ------------------------------
    // Optional: Call this when your exit spawns
    // ------------------------------
    public void SetExit(bool enable)
    {
        _exitPoint.gameObject.SetActive(enable);
    }
}


