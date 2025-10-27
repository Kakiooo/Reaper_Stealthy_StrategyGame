using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_Dragging_Testing : MonoBehaviour
{
    public GameObject Win, Instruct;
    public float Timer;
    public bool IsFinish;
    private void Awake()
    {
        Win.SetActive(false);
        IsFinish = false;
    }

    private void Update()
    {
        UIDisplay();
        if (IsFinish)
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

    public void UIDisplay()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Instruct.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tools")
        {
            Win.SetActive(true);
            IsFinish=true;
        }
    }
}
