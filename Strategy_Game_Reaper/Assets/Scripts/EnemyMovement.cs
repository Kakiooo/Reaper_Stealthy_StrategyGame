using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent _enemyAgent;
    public List<Transform> Step = new List<Transform>();
    [SerializeField] int _stepIndex = 0;
    [SerializeField] float _timer, _maxWait_time, _pushForce, _delayTime,_maxTime;
    [SerializeField] bool _getPushed;
    [SerializeField] Rigidbody _rb;
    private void Awake()
    {
        _delayTime = _maxTime;
    }
    void Start()
    {
        //_enemyAgent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        FollowSteps();
        //GetPush();
    }

    void FollowSteps()
    {
        _enemyAgent.destination = Step[_stepIndex].transform.position;
        if (_enemyAgent.remainingDistance <= 0)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _stepIndex++;
                if (_stepIndex > Step.Count - 1)
                {
                    _stepIndex = 0;
                }
                _timer = Random.Range(0, _maxWait_time);
            }

        }
    }


    private void GetPush()
    {

        if (_getPushed)
        {
            _delayTime -= Time.deltaTime;
            transform.position += transform.forward*_pushForce *Time.deltaTime;
            if( _delayTime <= 0)
            {
                _getPushed = false;
                _delayTime = 1;
            }
        }
    }
}
