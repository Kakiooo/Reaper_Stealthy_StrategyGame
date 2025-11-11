using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_blinking : MonoBehaviour
{
    [Header("UI Element to Blink")]
    public Image UiElement;      // The UI object you want to blink (e.g., Image, Text, etc.)

    [Header("Blink Settings")]
    public float BlinkInterval = 0.5f; // How fast it blinks
    public bool StartBlinking = false; // Control when blinking starts

    private float _timer;
    private bool _isVisible;

    void Update()
    {
        Blinking();
    }

    // Optional helper functions you can call from other scripts or button

    public void Blinking()
    {
        if (StartBlinking)
        {
            _timer += Time.deltaTime;
            print("start?Blink");
            if (_timer >= BlinkInterval)
            {
                // Toggle visibility
                _isVisible = !_isVisible;
                UiElement.enabled=_isVisible;

                // Reset timer
                _timer = 0f;
            }
        }
        else if (!StartBlinking && UiElement != null)
        {
            // Make sure UI stays visible when not blinking
            if (UiElement.enabled)
                UiElement.enabled = false;
        }
    }
}
