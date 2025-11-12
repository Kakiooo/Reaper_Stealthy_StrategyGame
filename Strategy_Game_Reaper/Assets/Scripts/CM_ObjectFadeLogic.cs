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
    GameObject _currentHitObject;
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
                if(_hit.transform.gameObject!= _currentHitObject)
                {
                   if(_currentHitObject != null) _currentHitObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //_wallBlocking = _hit.transform.gameObject.GetComponent<Obstacles_Fading>();
                    _hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    _currentHitObject = _hit.transform.gameObject;
                    CurrentHitObject_Name = _hit.transform.gameObject.name;
                }
                else
                {
                   _currentHitObject.GetComponent<MeshRenderer>().enabled= false;
                }
            }
            else if (_hit.transform.tag == "Player" && _currentHitObject != null)
            {
                //if(_wallBlocking!=null) _wallBlocking.StartFade = false;
                _currentHitObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

        }

    }
}
