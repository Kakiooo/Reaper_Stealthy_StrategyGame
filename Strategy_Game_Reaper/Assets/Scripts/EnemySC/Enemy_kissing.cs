using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_kissing : MonoBehaviour
{
    [Header("===Tweaking them Please!!!!==============================================================================================")]
    [SerializeField] float _maxKissingTime;

    [SerializeField] float _kissingTime;
    [SerializeField] Enemy_SelfState_Manager _e_M;
    [SerializeField] Image _kissSign;

    private void Awake()
    {
        _kissSign.gameObject.SetActive(false);  
        _kissingTime = _maxKissingTime;
    }

    private void Update()
    {
        if( _e_M.CurrentState==Enemy_SelfState_Manager.EnemyState.Kiss)
        {
            OnKissing();
        }
    }



    void OnKissing()
    {
        //if (_kissSign != null)
        //{
        //    _kissSign.gameObject.SetActive(true);
        //}
        _kissingTime-=Time.deltaTime;
        if (_kissingTime <= 0)
        {
            _e_M.CurrentState = Enemy_SelfState_Manager.EnemyState.Move;
            _kissSign.gameObject.SetActive(false);
            _kissingTime = _maxKissingTime;
        }      

    }
}
