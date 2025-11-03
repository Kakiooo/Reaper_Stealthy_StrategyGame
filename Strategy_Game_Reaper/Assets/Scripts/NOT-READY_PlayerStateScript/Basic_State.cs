using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_State 
{
    public abstract void StartSetup(PlayerStateManager PlayerState);
    public abstract void UpdateState(PlayerStateManager PlayerState);
}
