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
    public float CountDown;
    [SerializeField] RectTransform _end_CanvaUI;
    [SerializeField] GameManager _gameManager;
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
            _end_CanvaUI.anchoredPosition = Vector2.MoveTowards(_end_CanvaUI.anchoredPosition, Vector2.zero, Time.deltaTime * 1500);
        }
    }

    public void DoneChoice()
    {

    }
}
