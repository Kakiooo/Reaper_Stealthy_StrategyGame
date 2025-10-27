using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_TriggerArea_Stealthy : MonoBehaviour
{
    [SerializeField] GameObject _winShown;
    bool _isWin;
    private void Awake()
    {
        if (_winShown != null)
        {
            _winShown.SetActive(false);
        }

        _isWin = false;
    }
    private void Update()
    {
        if (_isWin)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("YouWin");
            _winShown.SetActive(true);
            _isWin = true;
        }
    }
}
