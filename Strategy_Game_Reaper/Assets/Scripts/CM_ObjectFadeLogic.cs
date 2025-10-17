using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_ObjectFadeLogic : MonoBehaviour
{
    Vector3 _cameraDir;
    [SerializeField] GameObject _p_Object;
    float _rayLength;
    RaycastHit _hit;
    [SerializeField]
    LayerMask _wallLayer;
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
                 
            if (_hit.transform.tag == "Player")
            {
                if(_wallBlocking!=null) _wallBlocking.StartFade = false;
            }
            else if (_hit.transform.tag == "Wall")
            {
                _wallBlocking = _hit.transform.gameObject.GetComponent<Obstacles_Fading>();
                CurrentHitObject_Name = _hit.transform.gameObject.name;
                _wallBlocking.StartFade = true;
            }
        }

    }
}
