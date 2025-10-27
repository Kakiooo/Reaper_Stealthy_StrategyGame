using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Observing_TestingSC : MonoBehaviour
{
    public GameObject ObservingUI,SpaceUI;
    public float Timer;
    private void Awake()
    {
        ObservingUI.SetActive(true);
        SpaceUI.SetActive(false);
    }
    private void Update()
    {
        UIDisplay();
        SwitchScene_Num();
    }
    public void UIDisplay()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            ObservingUI.SetActive(false);
            SpaceUI.SetActive(true);
        }

    }

    public void SwitchScene_Num()
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
