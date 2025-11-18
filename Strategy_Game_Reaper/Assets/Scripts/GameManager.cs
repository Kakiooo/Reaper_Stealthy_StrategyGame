using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool LoseLevel;
    public float CountDown;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LoseResult();
    }

    void LoseResult()
    {
        if (LoseLevel)
        {
            CountDown-=Time.deltaTime;
            if(CountDown <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

}
