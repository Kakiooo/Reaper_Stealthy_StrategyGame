using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuUI_MouseInput : MonoBehaviour
{
    [SerializeField] LayerMask _hitLayer;
    [SerializeField] Camera _cm;
    Ray _mouseRay;
    [SerializeField] Transform _currentOB;

    private void Update()
    {
       
    }
    public void MousePos(InputAction.CallbackContext ctx)
    {
        if (_currentOB != null)
        {
            _currentOB.GetComponent<MenuUI_ButtonFunction>().GetDetected = false;
        }// if the ray is not shooting on THAT object, change the getdetected state of object into false

        _mouseRay = Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>());
        Debug.DrawRay(_mouseRay.origin, _mouseRay.direction* 100);
        RaycastHit hit;
        if (Physics.Raycast(_mouseRay, out hit, 100, _hitLayer))
        {
            hit.transform.GetComponent<MenuUI_ButtonFunction>().GetDetected = true;
            _currentOB=hit.transform;//store the current detecting object
        }
    }

    public void MouseSelect(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(_mouseRay, out hit, 100, _hitLayer))
            {
                hit.transform.GetComponent<MenuUI_ButtonFunction>().GetChosen = true;
            }
        }
    }

}
