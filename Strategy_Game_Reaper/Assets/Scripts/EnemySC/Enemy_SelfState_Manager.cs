using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SelfState_Manager : MonoBehaviour
{
    public enum EnemyState
    {
        Move,
        Kiss
    }
    public EnemyState CurrentState;
}
