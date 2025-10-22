using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles_Fading : MonoBehaviour
{
    public bool StartFade;

    [SerializeField] Material _material;
    float _trasparency,_t;
    [Range(0.0f, 1f)] public float Transparency_Amount;
    [SerializeField] float _speed;
    [SerializeField] CM_ObjectFadeLogic _refCM;


    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _trasparency = _material.color.a;
        _speed = 0.5f;
    }
    private void Update()
    {
        Fading_DueToBlocking();
    }
    public void Fading_DueToBlocking()
    {
        if (StartFade&& _refCM.CurrentHitObject_Name == this.name)
        {
            _t += _speed * Time.deltaTime;
            _trasparency = Mathf.Lerp(1, Transparency_Amount, _t);         
        }
        else if (!StartFade)
        {
            _t += _speed * Time.deltaTime;
            _trasparency = Mathf.Lerp(_trasparency, 1 , _t);
        }
        if(_refCM.CurrentHitObject_Name != this.name)//if the hit object changes, make the object not transparent
        {
            StartFade = false;
        }
         _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, _trasparency);
    }///testingssssdasd
}
