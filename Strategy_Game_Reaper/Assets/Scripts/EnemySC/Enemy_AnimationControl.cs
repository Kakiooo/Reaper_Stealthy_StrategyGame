using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_AnimationControl : MonoBehaviour
{
    [SerializeField] PlayerMovement _refTo_P;
    EnemyMovement _refTo_E;
    Animator _e_Animator;
    public string Current_EnemyName; 
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
        if (Current_EnemyName == "Tomato")
        {
            Animation_EnemyFacing();
        }
        if (Current_EnemyName == "Potato")
        {
            Animation_EnemyFacing_Potato();
        }

    }

    void Animation_EnemyFacing()
    {
        float angle = CheckPlayerPos();      // -180 to +180
        float absAngle = Mathf.Abs(angle);   // ignore left/right for type checking

        // Reset first
        _e_Animator.SetBool("IsBack", false);
        _e_Animator.SetBool("IsSide", false);

        // FRONT
        if (absAngle <= 45f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        // SIDE
        else if (absAngle <= 135f)
        {
            _e_Animator.SetBool("IsSide", true);
            GetComponent<SpriteRenderer>().flipX = angle < 0; // left side flips
        }
        // BACK
        else
        {
            _e_Animator.SetBool("IsBack", true);
        }
    }
    void Animation_EnemyFacing_Potato()
    {
        float angle = CheckPlayerPos();      // -180 to +180
        float absAngle = Mathf.Abs(angle);   // ignore left/right for type checking

        // Reset first
        _e_Animator.SetBool("IsBack", false);
        _e_Animator.SetBool("IsSideRight", false);
        _e_Animator.SetBool("IsSideLeft", false);

        // FRONT
        if (absAngle <= 45f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        // SIDE
        else if (absAngle <= 135f)
        {
            _e_Animator.SetBool("IsSideLeft", true);
            _e_Animator.SetBool("IsSideRight", false);
            if (angle < 0)
            {
                _e_Animator.SetBool("IsSideRight", true);
                _e_Animator.SetBool("IsSideLeft", false);
            }

        }
        // BACK
        else
        {
            _e_Animator.SetBool("IsBack", true);
            _e_Animator.SetBool("IsSideLeft", false);
            _e_Animator.SetBool("IsSideRight", false);
        }
    }

    float CheckPlayerPos()
    {
        Vector3 playerPos = _refTo_P.transform.position-_refTo_E.transform.position;
        Vector3 enemyDir= _refTo_E.transform.forward;

        playerPos.y = 0;
        enemyDir.y = 0;

        float angleBetween=Vector3.SignedAngle(enemyDir,playerPos,Vector3.up);
        return angleBetween;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(_refTo_E.transform.position, _refTo_P.transform.position);
    }
}
