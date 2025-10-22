using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State_Idle : Basic_State_Enemy
{
    float _timer;
    float _maxWait_time;
    public override void Start_setup(Enemy_State_Manager E_State)
    {
        _maxWait_time = 5;
         _timer = _maxWait_time;
    }
    public override void Update_State(Enemy_State_Manager E_State)
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            E_State.Current_E_State=E_State.State_Walk_E;
            _timer = Random.Range(0, _maxWait_time);
        }
    }
}
