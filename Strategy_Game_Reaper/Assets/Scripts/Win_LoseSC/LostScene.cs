using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LostScene : MonoBehaviour
{
    public Image Higher_Sad1;
    float _timer;
    float _blinkInterval = 0.3f;
    bool _isVisible;

    private void Update()
    {
        blinkingImage();
        SwitchScene();
    }

    void blinkingImage()
    {
        _timer += Time.deltaTime;
        print("start?Blink");
        if (_timer >= _blinkInterval)
        {
            // Toggle visibility
            _isVisible = !_isVisible;
            Higher_Sad1.enabled = _isVisible;

            // Reset timer
            _timer = 0f;
        }
    }
    void SwitchScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
