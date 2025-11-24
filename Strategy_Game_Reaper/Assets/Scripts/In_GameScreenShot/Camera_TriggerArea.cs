using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_TriggerArea : MonoBehaviour
{

    [Header("===Tweaking them Please!!!!==============================================================================================")]

    public int Max_ShotNum;
    [SerializeField] int _detectRadius;

    [Header("Assign Values")]
    public int NumCameraShot;
    [SerializeField] PlayerMovement _p_Move;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _obstaclesLayer;
    [SerializeField] Transform _orig_Detect;
    [SerializeField] PlayerManager _p_Manager;
    [SerializeField] Ending_DisplayResult _endingPart;
    [SerializeField] CameraScreenShot _cameraScreenShot;    
    public bool ReachLimit_Shots;
    public bool IsConfirmPic;
    public bool CheckResult;
    infoGather _outcome;
    private void Awake()
    {
        ReachLimit_Shots = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _p_Manager.CurrentState == PlayerManager.PlayerState.CameraShot && !ReachLimit_Shots&& !_cameraScreenShot.IsCaptured)
        {
            CheckResult = true;
            ScoreCalculation();
        }
        BeyondLimits();


    }
    infoGather GatherEnemyInfo()//obstacles are causing some issue, so dont use that much obstacles
    {
        // Collider[] inRangeEnemy = Physics.OverlapBox(_orig_Detect.transform.position, _triggerBoxSize, Quaternion.identity, _enemyLayer, QueryTriggerInteraction.Collide);//!!!!!!!!size of BOX is WIRED !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Collider[] inRangeEnemy = Physics.OverlapCapsule(transform.position, _orig_Detect.position, _detectRadius, _enemyLayer, QueryTriggerInteraction.UseGlobal);//!!!!!!!!size of BOX is WIRED !!!!!!!!!!!!!!!!!!!!!!!!!!!!

        int numTarget = inRangeEnemy.Length;
        List<Vector3> targetPos = new List<Vector3>();
        List<Enemy_SelfState_Manager.EnemyState> targetState = new List<Enemy_SelfState_Manager.EnemyState>();
        List<float> dis = new List<float>();
        if (inRangeEnemy.Length != 0)
        {
            for (int i = 0; i <= inRangeEnemy.Length - 1; i++)
            {
                targetPos.Add(inRangeEnemy[i].transform.position);
                Vector3 dir = targetPos[i] - transform.position;
                if (!Physics.Raycast(_p_Move.transform.position, dir.normalized, dir.magnitude, _obstaclesLayer))//only when there is no obstacles in-between player and enemies
                {
                    targetState.Add(inRangeEnemy[i].transform.gameObject.GetComponent<Enemy_SelfState_Manager>().CurrentState);    //Collecting All the states
                    print("Detected_Enemy_Count:" + targetState.Count);
                }
                if (targetState.Count != 0) print(targetState[i]); //only when enemies are not behind the wall the enemy state will be recorded
            }
        }

        return new infoGather(dis, targetState);//get info about how far two enemies are, and what are the states 
    }

    void ScoreCalculation()
    {
        _outcome = GatherEnemyInfo();
        print("Number of enemy" + _outcome.TargetState.Count);
        if (CheckResult&&_outcome.TargetState.Count>1)
        {       
            if (_outcome.TargetState[0] == Enemy_SelfState_Manager.EnemyState.Kiss && _outcome.TargetState[1] == Enemy_SelfState_Manager.EnemyState.Kiss)
            {
                _endingPart.PicResults.Add(true);
            }
            else if (_outcome.TargetState[0] == Enemy_SelfState_Manager.EnemyState.Move || _outcome.TargetState[1] == Enemy_SelfState_Manager.EnemyState.Move)
            {
                _endingPart.PicResults.Add(false);
            }
            print("IsCalled");
            CheckResult = false;
        }
        else if ( CheckResult)
        {
            _endingPart.PicResults.Add(false);
            CheckResult = false;
        }
    
    }

    void BeyondLimits()
    {
        if (NumCameraShot >= Max_ShotNum)
        {
            ReachLimit_Shots = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(_orig_Detect.transform.position, _triggerBoxSize);
        Gizmos.DrawWireSphere(_orig_Detect.transform.position, _detectRadius);
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
        // Gizmos.DrawWireCube(_orig_Detect.transform.position, _triggerBoxSize);
    }

    struct infoGather
    {
        public List<float> Dis;
        public List<Enemy_SelfState_Manager.EnemyState> TargetState;
        public infoGather(List<float> dis, List<Enemy_SelfState_Manager.EnemyState> state)
        {
            Dis = dis;
            TargetState = state;
        }
    }
}
