using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_AnimationControl : MonoBehaviour
{
    [SerializeField] PlayerMovement _refTo_P;
    EnemyMovement _refTo_E;
    Animator _e_Animator;
    private void Awake()
    {
        _refTo_E = transform.GetComponentInParent<EnemyMovement>();
        _e_Animator = transform.GetComponent<Animator>(); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animation_EnemyFacing();
    }

    void Animation_EnemyFacing()
    {
        float angle=CheckPlayerPos();
        if (angle <= 60)
        {
            _e_Animator.SetBool("IsBack",false);
            _e_Animator.SetBool("IsSide", false);
            print("Work?");
        }
        else if(angle>60 && angle <=120)
        {
            _e_Animator.SetBool("IsSide", true);
            _e_Animator.SetBool("IsBack", false);
            print("Work1111?");
        }
        else if (120 < angle && angle <= 180)
        {
            _e_Animator.SetBool("IsBack", true);
            _e_Animator.SetBool("IsSide", false);
            print("Work?2222");
        }
    }

    float CheckPlayerPos()
    {
        Vector3 playerPos = _refTo_P.transform.position-_refTo_E.transform.position;
        Vector3 enemyDir= _refTo_E.transform.forward;

        playerPos.y = 0;
        enemyDir.y = 0;

        float angleBetween=Vector3.Angle(playerPos, enemyDir);
        return angleBetween;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_refTo_E.transform.position, _refTo_P.transform.position);
    }
}
