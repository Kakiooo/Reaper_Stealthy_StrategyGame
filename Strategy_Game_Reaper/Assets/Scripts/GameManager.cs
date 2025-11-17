using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool LoseLevel;
    public bool HasMade_Choice;
    public bool HasLoad_Everything;
    public bool ShownResultEnd_Menu;
    public float CountDown;
    [SerializeField] RectTransform _end_CanvaUI;
    public List<Enemy_SelfState_Manager.EnemyState> TargetState = new List<Enemy_SelfState_Manager.EnemyState>();

    public List<Image> ChoosePictures=new List<Image>();  
    public List<Texture2D> PictureTooken = new List<Texture2D>();
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LoseResult();
    }

    void LoseResult()
    {
        if (LoseLevel)
        {
            CountDown-=Time.deltaTime;
            if(CountDown <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

}
