using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]NavMeshAgent _enemyAgent;
    public List<Transform> Step=new List<Transform>();
    [SerializeField] int _stepIndex = 0;
    [SerializeField] float _timer,_maxWait_time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowSteps();
    }

    void FollowSteps()
    {
        _enemyAgent.destination = Step[_stepIndex].transform.position;
        if (_enemyAgent.remainingDistance <=0)
        {
            _timer-=Time.deltaTime;
            if (_timer < 0)
            {
                _stepIndex++;
                if (_stepIndex > Step.Count - 1)
                {
                    _stepIndex = 0;
                }
                _timer =Random.Range(0, _maxWait_time);
            }
           
        }
    }

    void GetAttacked()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tools")
        {
            print("Died");
        }
    }
}
