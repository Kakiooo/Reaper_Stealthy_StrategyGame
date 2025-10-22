using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] EnemyMovement _enemy_M;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tools")
        {
            print("Die");
            //_enemyAgent.isStopped = true;
        }
    }
}
