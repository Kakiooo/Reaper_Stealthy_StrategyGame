using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_MeetOtherEnemies : MonoBehaviour
{
    [SerializeField] Enemy_SelfState_Manager _e_M;
    [SerializeField] NavMeshAgent _n_M;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OtherCelebrity"))
        {
            _e_M.CurrentState = Enemy_SelfState_Manager.EnemyState.Kiss;
        }
    }
}
