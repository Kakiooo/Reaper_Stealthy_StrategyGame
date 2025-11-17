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
        print(angle);
        float frontAngle = 45;
        float sideAngle = 135;
        float backAngle = 180;
        if (angle>=0&&angle <= frontAngle)
        {
            _e_Animator.SetBool("IsBack",false);
            _e_Animator.SetBool("IsSide", false);
        }     
        else if(angle> frontAngle && angle <= sideAngle)
        {
            _e_Animator.SetBool("IsSide", true);
            _e_Animator.SetBool("IsBack", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (sideAngle < angle && angle <= backAngle)
        {
            _e_Animator.SetBool("IsBack", true);
            _e_Animator.SetBool("IsSide", false);

        }

        if (angle >= -frontAngle && angle<0)
        {
            _e_Animator.SetBool("IsBack", false);
            _e_Animator.SetBool("IsSide", false);
            print("Work?");
        }
        else if (angle <-frontAngle && angle >= -sideAngle)
        {
            _e_Animator.SetBool("IsSide", true);
            _e_Animator.SetBool("IsBack", false);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else if ( angle<-sideAngle && angle >=-backAngle)
        {
            _e_Animator.SetBool("IsBack", true);
            _e_Animator.SetBool("IsSide", false);
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
        Gizmos.DrawLine(_refTo_E.transform.position, _refTo_P.transform.position);
    }
}
