using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SelfState_Manager : MonoBehaviour
{
    [SerializeField] NavMeshAgent _e_Navi;
    [SerializeField] GameManager _gameManager;
    [SerializeField] float _rotateSpeed;
    public enum EnemyState
    {
        Move,
        Kiss,
        SpotIt,
        StopMoving
    }
    public EnemyState CurrentState;
    private void Update()
    {
        EnemyStateOnNAVI();
        StopMoving();
    }
    void EnemyStateOnNAVI()
    {
        if (CurrentState == EnemyState.SpotIt)
        {
            DancingModeActivate();
            _e_Navi.isStopped = true;
            if (_gameManager != null) _gameManager.LoseLevel = true;

        }
        else if (CurrentState == EnemyState.Move)
        {
            _e_Navi.isStopped = false;
        }
        else if (CurrentState == EnemyState.Kiss)
        {
            _e_Navi.isStopped = true;
        }
        else if (CurrentState == EnemyState.StopMoving)
        {
            _e_Navi.isStopped = false;
        }
    }
    void DancingModeActivate()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }

    void StopMoving()
    {
        if (_gameManager.CurrentState==GameManager.GameState.EndPhase)
        {
            CurrentState = EnemyState.StopMoving;
        }
    }
}
