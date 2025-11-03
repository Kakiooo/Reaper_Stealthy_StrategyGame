using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    Basic_State _current_State;
    Movement_State _state_Move=new Movement_State();
    private void Awake()
    {
        _current_State = _state_Move;
    }
    private void Update()
    {
        _current_State.UpdateState(this);
    }
}
