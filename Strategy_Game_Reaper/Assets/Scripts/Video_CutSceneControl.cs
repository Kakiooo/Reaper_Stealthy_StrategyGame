using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video_CutSceneControl : MonoBehaviour
{
    public GameObject SpaceInstruction;
    GameManager _gameManager;
    public VideoPlayer Video;
    public bool VideoIsEnd;
    public float CountDown;
    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpaceInstruction.gameObject.SetActive(false);

        //Video.Stop();
        //Video.time = 0;
        //Video.Play();

    }
    private void Update()
    {
        VideoWait();
        if (SceneManager.GetActiveScene().name == "Video_Spotted")
        {
            if(Input.GetKeyDown(KeyCode.Space) && VideoIsEnd)
            {
                SceneManager.LoadScene("InGame");
            }
        }else
        {
            if (Input.GetKeyDown(KeyCode.Space) && VideoIsEnd)
            {
                _gameManager.SwitchingScene();
            }
        }
       
    }

    private void Start()
    {
        Video.Prepare();
        Video.loopPointReached += showSpaceBar;

    }


    void showSpaceBar(VideoPlayer vp)
    {
        SpaceInstruction.SetActive(true);
        VideoIsEnd=true;
        print("isthere");
    }

    void VideoWait()
    {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0)
        {
            VideoIsEnd = true;
            SpaceInstruction.SetActive(true);
            CountDown = 8;
        }
    }
}
