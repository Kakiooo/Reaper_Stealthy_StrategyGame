using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending_DisplayResult : MonoBehaviour
{
    public bool HasMade_Choice;
    public bool HasLoad_Everything;
    public bool ShownResultEnd_Menu;
    [SerializeField] private bool _resultMove;
    public float CountDown;
    public TextMeshProUGUI TimeResult;
    public GameObject CaptureImageDisplay;
    [SerializeField] RectTransform _end_CanvaUI;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Image _gameWin;
    [SerializeField] Image _gameLose;
    [SerializeField] List<Image> _photoFrames = new List<Image>();

    public List<bool> PicResults = new List<bool>();

    public List<Image> ChoosePictures = new List<Image>();
    public List<Texture2D> PictureTooken = new List<Texture2D>();
    public int ChoosePic;

    public List<Button> ChooseButton=new List<Button>();    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (ShownResultEnd_Menu && !HasLoad_Everything) SetUpOptions();

        DisplayOptions();
        MovetoCenterResult();
    }

    public void SetUpOptions()
    {
        CaptureImageDisplay.gameObject.SetActive(false);
        _gameManager.CurrentState = GameManager.GameState.EndPhase;
        if (PictureTooken.Count == 0) return;
        for (int i = 0; i <= PictureTooken.Count - 1; i++)
        {
            Sprite newImage = Sprite.Create(PictureTooken[i], new Rect(0, 0, PictureTooken[i].width, PictureTooken[i].height), new Vector2(0.5f, 0.5f)); //creating new sprite by using texture 2d
            ChoosePictures[i].sprite = newImage;
        }
        HasLoad_Everything = true;
    }
    public void DisplayOptions()
    {
        if (HasLoad_Everything)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _end_CanvaUI.DOAnchorPos(Vector2.zero, 0.5f);
        }
    }

    public void Choice(int index)
    {
        //HandleChoice(i, _photoFrame1, _photoFrame2); //Can make a list of ALL image Options
        ChoosePic = index;
        for (int i = 0; i <= _photoFrames.Count - 1; i++)
        {
            if (i != index) _photoFrames[i].gameObject.SetActive(false);
        }

        _resultMove = true;
    }

    public void MovetoCenterResult()
    {
        if (_resultMove)
        {
            RectTransform Rect = _photoFrames[ChoosePic].transform.GetComponent<RectTransform>();
            ChooseButton[ChoosePic].gameObject.SetActive(false);
            if (PicResults[ChoosePic]) _gameWin.enabled = true;
            else _gameLose.enabled = true;

            Vector2 targetPos = new Vector2(0, 45);
            TimeResult.text = "Time Cost: " + _gameManager.Timer_Result;
            Rect.DOAnchorPos(targetPos, 0.5f, false);
        }

    }
}
