using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] RectTransform _end_CanvaUI;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Image _photoFrame1;//too dependent need to make it as list
    [SerializeField] Image _photoFrame2;//too dependent need to make it as list
    public List<bool> PicResults = new List<bool>();

    public List<Image> ChoosePictures = new List<Image>();
    public List<Texture2D> PictureTooken = new List<Texture2D>();
    
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
            _end_CanvaUI.anchoredPosition = Vector2.MoveTowards(_end_CanvaUI.anchoredPosition, Vector2.zero, Time.deltaTime * 1500);
        }
    }

    public void ChoiceFirst()
    {
        HandleChoice(0, _photoFrame1, _photoFrame2); //Can make a list of ALL image Options
    }
    public void ChoiceSecond()
    {
        HandleChoice(1, _photoFrame2, _photoFrame1);//Can make a list of all Image Options
    }

    private void HandleChoice(int index, Image chosenFrame, Image otherFrame)
    {
        otherFrame.gameObject.SetActive(false);

        Debug.Log(PicResults[index] ? "WinGame" : "Lose Game");

        _resultMove = true;
    }

    public void MovetoCenterResult()
    {
        if (_resultMove)
        {
            if (_photoFrame1.gameObject.activeSelf==true)
            {
                RectTransform Rect = _photoFrame1.transform.GetComponent<RectTransform>();
                Rect.DOAnchorPos(Vector2.zero, 0.5f, false);
                print("IsitWork");
            }
            else if (_photoFrame2.gameObject.activeSelf == true)
            {
                RectTransform Rect = _photoFrame2.transform.GetComponent<RectTransform>();
                Rect.DOAnchorPos(Vector2.zero, 0.5f, false);
            }
        }

    }
}
