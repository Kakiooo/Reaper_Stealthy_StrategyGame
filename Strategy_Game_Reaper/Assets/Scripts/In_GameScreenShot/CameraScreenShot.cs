using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraScreenShot : MonoBehaviour
{
    [SerializeField] Image _camera_Capture;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine("CaptureThePhoto");
        }
    }
    IEnumerator CaptureThePhoto()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture();
        Texture2D newScreenShot=new Texture2D(screenShot.width,screenShot.height,TextureFormat.RGBA32,false);

        newScreenShot.SetPixels(screenShot.GetPixels());//Make the new texture has the same pixel as captured image
        newScreenShot.Apply();

        Destroy(screenShot);

        Sprite newImage = Sprite.Create(newScreenShot, new Rect(0, 0, newScreenShot.width, newScreenShot.height), new Vector2(0.5f, 0.5f));
        _camera_Capture.enabled = true; 
        _camera_Capture.sprite = newImage;
        float buffer_X = newScreenShot.width / 10;
        float buffer_Y = newScreenShot.height / 10;
        _camera_Capture.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(newScreenShot.width- buffer_X, newScreenShot.height- buffer_Y);
    }
}
