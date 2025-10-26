using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class FOW_Enemy : MonoBehaviour
{
    public float ViewRadius;
    [Range(0,360)]
    public float ViewAngle;
    [SerializeField] LayerMask _obstacles, _target;
    public List<Transform> VisibleObjects=new List<Transform> ();

    private void Start()
    {
        StartCoroutine("LineOfSight", 0.2f);
    }
    private void Update()
    {
    }
    
    IEnumerator LineOfSight(float Delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            DetectPlayer();
        }
    }
    public Vector3 AngleDir(float angleDegree,bool IsGlobalAngle)
    {
        if (!IsGlobalAngle)
        {
            angleDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegree * Mathf.Deg2Rad));
    }

    public void DetectPlayer()
    {
        VisibleObjects.Clear ();
        Collider[] InTheRangeObjects=Physics.OverlapSphere(transform.position,ViewRadius,_target);
        for(int i = 0; i < InTheRangeObjects.Length; i++)
        {
            Transform target= InTheRangeObjects[i].transform;
            Vector3 targetDir=(target.transform.position-transform.position).normalized;//GEt direction of that 
            float betweenAngle=Vector3.Angle(transform.forward, targetDir);//check if the target is inside the Line of sight angle
            if(betweenAngle < (ViewAngle/2))
            {
                float dis = Vector3.Distance(target.transform.position, transform.position);
                if (!Physics.Raycast(transform.position, targetDir, dis, _obstacles))//if there is no obstacles inbetween target and object with vision
                {
                    VisibleObjects.Add(target); //It means the target has been seen
                }
            }
        }
    }

}
