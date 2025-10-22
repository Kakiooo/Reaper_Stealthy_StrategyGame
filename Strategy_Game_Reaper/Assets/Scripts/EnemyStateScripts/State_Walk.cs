using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Walk : Basic_State_Enemy
{
    [SerializeField] NavMeshAgent _enemyAgent;
    int _stepIndex;
    public override void Start_setup(Enemy_State_Manager E_State)
    {
        _enemyAgent=E_State.transform.GetComponent<NavMeshAgent>();
        _stepIndex = _stepIndex+1;
    }
    public override void Update_State(Enemy_State_Manager E_State)
    {
        _enemyAgent.destination = E_State.Step[_stepIndex].transform.position;
        if (_enemyAgent.remainingDistance <= 0)
        {
            E_State.Current_E_State = E_State.State_Idle_E;
        }
    }
}
