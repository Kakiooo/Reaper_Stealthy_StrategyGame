using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SelfState_Manager : MonoBehaviour
{
    [SerializeField] NavMeshAgent _e_Navi;
    public enum EnemyState
    {
        Move,
        Kiss,
        SpotIt
    }
    public EnemyState CurrentState;
    private void Update()
    {
        EnemyStateOnNAVI();
    }
    void EnemyStateOnNAVI()
    {
        if(CurrentState == EnemyState.SpotIt)
        {
            _e_Navi.isStopped = true;
        }
       else if (CurrentState == EnemyState.Move)
        {
            _e_Navi.isStopped = false;
        }
        else if (CurrentState == EnemyState.Kiss)
        {
            _e_Navi.isStopped = true;
        }
    }
}
