using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_ObjectFadeLogic : MonoBehaviour
{
    [Header("Assign Values")]
    Vector3 _cameraDir;
    [SerializeField] GameObject _p_Object;
    [SerializeField] LayerMask _wallLayer;
    float _rayLength;
    RaycastHit _hit;
    public bool CMStartFade;
    public string HitObject;
    Obstacles_Fading _wallBlocking;
    public string CurrentHitObject_Name;
    private void Update()
    {
        DetectWall();
    }
    public void DetectWall()
    {
        _cameraDir=_p_Object.transform.position-transform.position;
        _rayLength=_cameraDir.magnitude;

        if (Physics.Raycast(transform.position,_cameraDir, out _hit, _rayLength))
        {
            if (_hit.transform.tag == "Wall")
            {
                print("Working?");
                //_wallBlocking = _hit.transform.gameObject.GetComponent<Obstacles_Fading>();
                _hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                CurrentHitObject_Name = _hit.transform.gameObject.name;
                //_wallBlocking.StartFade = true;
            }
            else if (_hit.transform.tag == "Player")
            {
                //if(_wallBlocking!=null) _wallBlocking.StartFade = false;
                _hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

        }

    }
}
