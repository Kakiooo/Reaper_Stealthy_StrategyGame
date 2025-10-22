using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State_Manager : MonoBehaviour
{
    public Basic_State_Enemy Current_E_State;
    public State_Walk State_Walk_E=new State_Walk();
    public State_Walk State_Idle_E = new State_Walk();


    public List<Transform> Step = new List<Transform>();
    void Start()
    {
        Current_E_State = State_Walk_E;
        Current_E_State.Start_setup(this);
    }

    // Update is called once per frame
    void Update()
    {     
        Current_E_State.Update_State(this);
    }
}
