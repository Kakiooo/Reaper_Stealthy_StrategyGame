using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Threading;

public class CameraScreenShot : MonoBehaviour
{
    [Header("===Tweaking them Please!!!!==============================================================================================")]
    [SerializeField] float _maxLastTime;
    [SerializeField] float _maxObserveTime;

    [Header("Assign Values")]
    [SerializeField] Image _camera_Capture;
    [SerializeField] Image _cameraFrame;
    [SerializeField] Image _cameraFlash;
    [SerializeField] Image _capTuredImage;
    [SerializeField] bool _isCaptured;
    float _flash_A, _tFlash;
    [SerializeField] Camera_TriggerArea _cm_Detection;
   
    [SerializeField] PlayerManager _p_State;
    [SerializeField] GameObject _warningBar;
    [SerializeField] Slider _warningBarSlider;
    [SerializeField] float _timer_ShotPic;
    [SerializeField] float _warningCountDown;
    [SerializeField] GameManager _r_G;



    private void Awake()
    {
        _flash_A = 1;
        _cameraFlash.gameObject.SetActive(false);
        _capTuredImage.enabled = false;
        _capTuredImage.transform.GetChild(0).gameObject.SetActive(false);
        _timer_ShotPic = _maxLastTime;
        _warningCountDown = _maxObserveTime;
    }

    private void Update()
    {
        Flash_CameraUI(2);
        ConfirmPic();

        if (_p_State.CurrentState == PlayerManager.PlayerState.CameraShot)
        {
            _warningBar.gameObject.SetActive(true);
            _warningShotTimeLimit();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !_isCaptured&& !_cm_Detection.ReachLimit_Shots)
            {
                StartCoroutine("CaptureThePhoto");
                _isCaptured = true;
            }
        }
        else
        {
            _warningBar.gameObject.SetActive(false);
        }


        ImageLasting();


    }
    IEnumerator CaptureThePhoto()///How many times are available for taking pictures is not set
    {
        _cameraFrame.gameObject.SetActive(false);//Hide UI
        _warningBar.gameObject.SetActive(false);
        _capTuredImage.enabled = false;
        _capTuredImage.transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();

        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture();
        ScreenCapture.CaptureScreenshot("Assets/INGAMEScreen_Shot"+".png");
        Texture2D newScreenShot = new Texture2D(screenShot.width, screenShot.height, TextureFormat.RGBA32, false);//make the new tecture has proper color by USING TextureFormat

        newScreenShot.SetPixels(screenShot.GetPixels());//Make the new texture has the same pixel as captured image
        newScreenShot.Apply();

        Destroy(screenShot);

        Sprite newImage = Sprite.Create(newScreenShot, new Rect(0, 0, newScreenShot.width, newScreenShot.height), new Vector2(0.5f, 0.5f)); //creating new sprite by using texture 2d
        _camera_Capture.enabled = true;
        _camera_Capture.sprite = newImage;//assign the image

        float buffer_X = newScreenShot.width / 10;//make the screenshot smaller so that it can be identified.
        float buffer_Y = newScreenShot.height / 10;
        _camera_Capture.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(newScreenShot.width - buffer_X, newScreenShot.height - buffer_Y);//make the picuture be shown with the smaller size but same ratio as the resolution of screen

        _capTuredImage.enabled = true;
        _capTuredImage.sprite = newImage;

        _cameraFlash.gameObject.SetActive(true);//show UI after hide it for screenshot
        _cameraFrame.gameObject.SetActive(true);
        _capTuredImage.transform.GetChild(0).gameObject.SetActive(true);
    }

    void Flash_CameraUI(float FadeSpeed)
    {
        if (_p_State.CurrentState == PlayerManager.PlayerState.CameraShot)
        {
            _cameraFrame.gameObject.SetActive(true);
        }
        else _cameraFrame.gameObject.SetActive(false);

        if (_cameraFlash.isActiveAndEnabled)
        {
            float targetTransparency = 0;
            _tFlash += Time.deltaTime / FadeSpeed;
            _flash_A = Mathf.Lerp(_flash_A, targetTransparency, _tFlash);
            if (_flash_A == targetTransparency)
            {
                _cameraFlash.gameObject.SetActive(false);
                _tFlash = 0;
                _flash_A = 1;
            }
            _cameraFlash.color = new Color(_cameraFlash.color.r, _cameraFlash.color.g, _cameraFlash.color.b, _flash_A);
        }
    }

    void _warningShotTimeLimit() //the longer you stay, the easier you will get spotted
    {
        if (_warningBar.gameObject.activeSelf)
        {
            if (!_isCaptured)
            {
                _warningCountDown -= Time.deltaTime;
                _warningBarSlider.value = _warningCountDown;
                //stop the count down and switch
            }
            if (_warningBarSlider.value <= 0)
            {
                print("time is End");
                _r_G.LoseLevel=true;
                //need to force to back to walking state or lose state
            }
        }
        else
        {
            _warningBarSlider.value = _maxObserveTime;
            _warningCountDown = _maxObserveTime;
        }


    }


    void ImageLasting()
    {
        Timer counting = CountDown(_isCaptured, _timer_ShotPic, _maxLastTime);
        _isCaptured = counting.IsActive;
        _timer_ShotPic = counting.Time;

        if (!_isCaptured && _camera_Capture.sprite != null && _camera_Capture.isActiveAndEnabled)
        {
            _camera_Capture.enabled = false;
            _camera_Capture.sprite = null;
        }

    }//how long the screen shot will disappear
    public struct Timer //used to store multiple types of variables for return
    {
        public float Time;
        public bool IsActive;
        public Timer(float time, bool isActive)
        {
            Time = time;
            IsActive = isActive;
        }
    }

    Timer CountDown(bool control, float Timer, float MaxTime)
    {
        if (control)
        {
            Timer -= Time.deltaTime;
            if (Timer < 0)
            {
                control = false;
                Timer = MaxTime;
            }
        }
        return new Timer(Timer, control);//can only return the value that Struc contains
    }

    void ConfirmPic()//May change after this testing phase
    {
        if(_capTuredImage.sprite != null && Input.GetKeyDown(KeyCode.Space))
        {
            _cm_Detection.IsConfirmPic = true;
        } 
    }
}
