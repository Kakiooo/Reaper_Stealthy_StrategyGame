using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_TriggerArea : MonoBehaviour
{
    [SerializeField] PlayerMovement _p_Move;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] Transform _orig_Detect;
    [SerializeField] Vector3 _triggerBoxSize;
    [SerializeField] PlayerManager _p_Manager;
    private void Awake()
    { 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& _p_Manager.CurrentState==PlayerManager.PlayerState.CameraShot)
        {
            GatherEnemyInfo();
        }

    }
    infoGather GatherEnemyInfo()
    {
        Collider[] inRangeEnemy = Physics.OverlapBox(_orig_Detect.transform.position, _triggerBoxSize, Quaternion.identity, _enemyLayer, QueryTriggerInteraction.Collide);//!!!!!!!!size of BOX is WIRED !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        int numTarget = inRangeEnemy.Length;
        List< Vector3 > targetPos= new List< Vector3 >();
        List<Enemy_SelfState_Manager.EnemyState> targetState=new List<Enemy_SelfState_Manager.EnemyState> ();   
        List<float> dis= new List< float >();   
        if(inRangeEnemy.Length !=0)
        {
            for (int i = 0; i <= inRangeEnemy.Length-1; i++)
            {
                targetPos.Add(inRangeEnemy[i].transform.position);
                targetState.Add(inRangeEnemy[i].transform.gameObject.GetComponent<Enemy_SelfState_Manager>().CurrentState);    //Collecting All the states
                print(targetState[i]);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_orig_Detect.transform.position, _triggerBoxSize);
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
