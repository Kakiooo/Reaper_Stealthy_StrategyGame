using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [Header("===Tweaking them Please!!!!==============================================================================================")]
    [SerializeField] float _maxWait_time;

    [Header("Assign Values==============================================================================================")]
    [SerializeField] NavMeshAgent _enemyAgent;
    public List<Transform> Step = new List<Transform>();
    [SerializeField] int _stepIndex = 0;

    [SerializeField] bool  _forwardRound_Finish;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _timer_Waiting;
    [SerializeField] Enemy_SelfState_Manager _e_M;

    private void Awake()
    {
        _timer_Waiting = _maxWait_time;
    }
    void Start()
    {
        //_enemyAgent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_e_M.CurrentState == Enemy_SelfState_Manager.EnemyState.Move)
        {
            FollowSteps();
        }

        //GetPush();
    }

    void FollowSteps()
    {
        _enemyAgent.destination = Step[_stepIndex].transform.position;
        if (_enemyAgent.remainingDistance <= 0)
        {
            _timer_Waiting -= Time.deltaTime;
            if (_timer_Waiting < 0)
            {
                if (!_forwardRound_Finish)
                {
                    _stepIndex++;
                }
                else
                {
                    _stepIndex--;
                }
                _timer_Waiting = Random.Range(0, _maxWait_time);

            }
            if (_stepIndex > Step.Count - 1)
            {
                _forwardRound_Finish = true;
                _stepIndex--;
            }
            if (_stepIndex < 0)
            {
                _forwardRound_Finish = false;
                _stepIndex++;
            }

        }
    }
}
