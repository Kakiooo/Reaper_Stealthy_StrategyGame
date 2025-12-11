using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyThis : MonoBehaviour
{
    private static DontDestroyThis _instance;
    private static int _originalSceneIndex;

    private void Awake()
    {
        int activeIndex = SceneManager.GetActiveScene().buildIndex;


        if (_instance == null)
        {
            _instance = this;
            _originalSceneIndex = activeIndex;

            DontDestroyOnLoad(gameObject);
            return;
        }


        if (activeIndex == _originalSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
