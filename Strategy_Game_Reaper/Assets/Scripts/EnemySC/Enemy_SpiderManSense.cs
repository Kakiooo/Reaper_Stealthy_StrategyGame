using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_SpiderManSense : MonoBehaviour
{
    [Header("===Tweaking them Please!!!!==============================================================================================")]
    [SerializeField] float _maxDetect_Dis;
    [SerializeField] float _max_PStayingTime; //??????!~!!!!! This should Always smaller than stay time 

    [Header("Assign Values")]
    [SerializeField] GameObject _p_G_Sight;
    [SerializeField] GameObject _p_G;
    [SerializeField] GameObject _sight;
    [SerializeField] GameObject _visualSight_Indicator;

    [SerializeField] float _detectTimer;
    [SerializeField] bool _letRotate;
    [SerializeField] bool _hideBehindObjects;
    [SerializeField] bool _isWallBetween;
    [SerializeField] float _rotateSpeed;
    [SerializeField] Enemy_SelfState_Manager _e_Manager;
    [SerializeField] LayerMask _obstaclesLayer;
    [SerializeField] LayerMask _movingItemsLayer;
    private void Awake()
    {
        _detectTimer = _max_PStayingTime;
    }
    private void Update()
    {
        DancingModeActivate();
        Player_InCircle_Detection();
        ObstaclesDetection();
        VisualizeDetectRance();
    }
    /// <summary>
    /// Need to detect two layers of objects
    /// 1. detect if there is moving object between player and enemy (sight is higher to make crouch functional)
    /// 2.detect if there is wall between player and enemy (just for cancel detection when player is behind the wall)
    /// </summary>
    void Player_InCircle_Detection() ///Need Visual to show enemy circle and time Countdown for awaring players
    {
        float dis=Vector3.Distance(_p_G_Sight.transform.position,transform.position);

        if(dis <_maxDetect_Dis&& !_hideBehindObjects)//detect if player is hiding behind movable object
        {
            if (!_isWallBetween) //only when there is no wall then player can be detected in range of enemy
            {
                _detectTimer -= Time.deltaTime;
                if (_detectTimer < 0)
                {
                    _letRotate = true;
                    _e_Manager.CurrentState = Enemy_SelfState_Manager.EnemyState.SpotIt;
                }
            }
        }else if(dis > _maxDetect_Dis&& !_hideBehindObjects)
        {
            _detectTimer = _max_PStayingTime;
            _letRotate = false;
            _e_Manager.CurrentState = Enemy_SelfState_Manager.EnemyState.Move;
        }
    }

   void VisualizeDetectRance()
    {
        Vector2 size=new Vector2(_maxDetect_Dis, _maxDetect_Dis);
        _visualSight_Indicator.transform.localScale = size;
    }

    void DancingModeActivate()
    {
        if (_letRotate) transform.Rotate(Vector3.up * _rotateSpeed*Time.deltaTime);
    }

    void ObstaclesDetection()
    {
        Vector3 dir = _p_G_Sight.transform.position-_sight.transform.position;
        Debug.DrawRay(_sight.transform.position, dir*100);
        if(Physics.Raycast(_sight.transform.position, dir, Vector3.Distance(_sight.transform.position, _p_G_Sight.transform.position), _movingItemsLayer)) //change to moving item Layer
        {
            _hideBehindObjects=true;
        }
        else _hideBehindObjects = false;


        Vector3 dirWall = _p_G.transform.position -transform.position;
        if (Physics.Raycast(transform.position, dir, Vector3.Distance(transform.position, _p_G.transform.position), _obstaclesLayer))
        {
            _isWallBetween = true;
        }else _isWallBetween = false;
    }
}
