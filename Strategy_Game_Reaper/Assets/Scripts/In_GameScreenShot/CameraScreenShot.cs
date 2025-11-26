using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using TMPro;

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
    [SerializeField] TextMeshProUGUI _photoPaperNum;
    public bool IsCaptured;
    float _flash_A, _tFlash;
    [SerializeField] Camera_TriggerArea _cm_Detection;
   
    [SerializeField] PlayerManager _p_State;
    [SerializeField] GameObject _warningBar;
    [SerializeField] Slider _warningBarSlider;
    [SerializeField] float _timer_ShotPic;
    [SerializeField] float _warningCountDown;
    [SerializeField] GameManager _r_G;
    [SerializeField] Ending_DisplayResult _endingResult;
    public List<Texture2D> PictureTooken=new List<Texture2D>();
    public AudioSource CameraClick;


    private void Awake()
    {
        _flash_A = 1;
        _cameraFlash.gameObject.SetActive(false);
        _photoPaperNum.gameObject.SetActive(false);
        _capTuredImage.enabled = false;
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
            _photoPaperNum.gameObject.SetActive(true);
            int paperLeft=_cm_Detection.Max_ShotNum-_cm_Detection.NumCameraShot;
            _photoPaperNum.text = "Photo paper Left: " + paperLeft;
            if (Input.GetKeyDown(KeyCode.Mouse0) && !IsCaptured && !_cm_Detection.ReachLimit_Shots)
            {
               // print("Flash");
                StartCoroutine("CaptureThePhoto");
                CameraClick.Play();
                IsCaptured = true;
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
        _photoPaperNum.gameObject .SetActive(false);
        _capTuredImage.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        //ScreenCapture.CaptureScreenshot("Assets/INGAME_ScreenShot"+".png");
        Texture2D newScreenShot = new Texture2D(screenShot.width, screenShot.height, TextureFormat.RGBA32, false);//make the new tecture has proper color by USING TextureFormat

        _endingResult.PictureTooken.Add(newScreenShot);
        newScreenShot.SetPixels(screenShot.GetPixels());//Make the new texture has the same pixel as captured image
        newScreenShot.Apply();

        Destroy(screenShot);

        Sprite newImage = Sprite.Create(newScreenShot, new Rect(0, 0, newScreenShot.width, newScreenShot.height), new Vector2(0.5f, 0.5f)); //creating new sprite by using texture 2d
        _camera_Capture.enabled = true;
        _camera_Capture.sprite = newImage;//assign the image

        float buffer_X = newScreenShot.width / 5;//make the screenshot smaller so that it can be identified.
        float buffer_Y = newScreenShot.height / 5;
        _camera_Capture.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(newScreenShot.width - buffer_X, newScreenShot.height - buffer_Y);//make the picuture be shown with the smaller size but same ratio as the resolution of screen

        _capTuredImage.enabled = true;
        _capTuredImage.sprite = newImage;

        _cameraFlash.gameObject.SetActive(true);//show UI after hide it for screenshot
        _cameraFrame.gameObject.SetActive(true);
        _photoPaperNum.gameObject.SetActive(true);
        _cm_Detection.NumCameraShot++;
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
            if (!IsCaptured)
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
        Timer counting = CountDown(IsCaptured, _timer_ShotPic, _maxLastTime);
        IsCaptured = counting.IsActive;
        _timer_ShotPic = counting.Time;

        if (!IsCaptured && _camera_Capture.sprite != null && _camera_Capture.isActiveAndEnabled)
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
    }
}
