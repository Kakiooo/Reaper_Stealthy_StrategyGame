using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuUI_ButtonFunction : MonoBehaviour
{
    public bool GetChosen,GetDetected;
    [SerializeField] string _currentHitOption;
    [SerializeField] CinemachineVirtualCamera _menuCm;
    Color _ogColor;
    private void Awake()
    {
        _currentHitOption=gameObject.name;
        _ogColor = transform.GetComponent<MeshRenderer>().material.color;
    }
    private void Update()
    {
        ColorChangeWhenSelect();

        switch (_currentHitOption)
        {
            case "Store":
                Store_GetChosen();
                break;
            case "Store_GoBack":
                Store_GoBack();
                break;
        }

    }

    void ColorChangeWhenSelect()
    {
        if (GetDetected)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else transform.GetComponent<MeshRenderer>().material.color = _ogColor;
    }
    void Store_GetChosen()
    {
        if (GetChosen)
        {
            _menuCm.gameObject.SetActive(false);
            GetChosen=false;
        }
    }
    void Store_GoBack()
    {
        if (GetChosen)
        {
            _menuCm.gameObject.SetActive(true);
            GetChosen = false;
        }
    }
}
