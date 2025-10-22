using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_State_Enemy
{
    public abstract void Start_setup(Enemy_State_Manager E_State);

    public abstract void Update_State(Enemy_State_Manager E_State);

}
