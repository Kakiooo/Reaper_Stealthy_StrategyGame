using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Camera_TriggerArea : MonoBehaviour
{
    [SerializeField] PlayerMovement _p_Move;
    [SerializeField] LayerMask _enemyLayer,_obstaclesLayer;
    [SerializeField] Transform _orig_Detect;
    [SerializeField] Vector3 _triggerBoxSize;
    [SerializeField] PlayerManager _p_Manager;
    public bool ReachLimit_Shots;
    [SerializeField] int _numCameraShot,_max_ShotTime;
    private void Awake()
    {
        ReachLimit_Shots = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& _p_Manager.CurrentState==PlayerManager.PlayerState.CameraShot&&!ReachLimit_Shots)
        {
            GatherEnemyInfo();
        }
        BeyondLimits();
    }
    infoGather GatherEnemyInfo()
    {
        _numCameraShot++; //count how many shots have been token
        // Collider[] inRangeEnemy = Physics.OverlapBox(_orig_Detect.transform.position, _triggerBoxSize, Quaternion.identity, _enemyLayer, QueryTriggerInteraction.Collide);//!!!!!!!!size of BOX is WIRED !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Collider[] inRangeEnemy = Physics.OverlapCapsule(transform.position,_orig_Detect.position,4,_enemyLayer,QueryTriggerInteraction.UseGlobal);//!!!!!!!!size of BOX is WIRED !!!!!!!!!!!!!!!!!!!!!!!!!!!!

        int numTarget = inRangeEnemy.Length;
        List< Vector3 > targetPos= new List< Vector3 >();
        List<Enemy_SelfState_Manager.EnemyState> targetState=new List<Enemy_SelfState_Manager.EnemyState> ();   
        List<float> dis= new List< float >();   
        if(inRangeEnemy.Length !=0)
        {
            for (int i = 0; i <= inRangeEnemy.Length-1; i++)
            {
                targetPos.Add(inRangeEnemy[i].transform.position);
                if (!Physics.Raycast(targetPos[i], _p_Move.transform.position, Vector3.Distance(targetPos[i], _p_Move.transform.position), _obstaclesLayer))//only when there is no obstacles in-between player and enemies
                {
                    targetState.Add(inRangeEnemy[i].transform.gameObject.GetComponent<Enemy_SelfState_Manager>().CurrentState);    //Collecting All the states
                }
                if (targetState.Count!=0) print(targetState[i]); //only when enemies are not behind the wall the enemy state will be recorded

            }
            if (targetPos.Count >= 2)
            {
                for (int i = 0; i <= targetPos.Count - 2; i++)
                {
                    float currentDis = Vector3.Distance(targetPos[i], targetPos[i + 1]);
                    dis.Add(currentDis);  //Collecting all the distance
                }
            }           
        }
        return new infoGather(dis, targetState);//get info about how far two enemies are, and what are the states 
    }

    void ScoreCalculation()
    {

    }

    void BeyondLimits()
    {
        if(_numCameraShot >= _max_ShotTime)
        {
            ReachLimit_Shots = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(_orig_Detect.transform.position, _triggerBoxSize);
        Gizmos.DrawWireSphere(_orig_Detect.transform.position, 4);
        Gizmos.DrawWireSphere(transform.position,4);
        // Gizmos.DrawWireCube(_orig_Detect.transform.position, _triggerBoxSize);
    }

    struct infoGather
    {
        List<float> _dis;
        List<Enemy_SelfState_Manager.EnemyState> _targetState;
        public infoGather(List<float> dis, List<Enemy_SelfState_Manager.EnemyState> state)
        {
            _dis = dis;
            _targetState = state;   
        }
    }
}
