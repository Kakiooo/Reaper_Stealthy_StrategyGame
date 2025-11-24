using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_CutSceneControl : MonoBehaviour
{
    public GameObject SpaceInstruction;
    GameManager _gameManager;
    public VideoPlayer Video;
    public bool VideoIsEnd;
    private void Awake()
    {
        _gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();    
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& VideoIsEnd)
        {
            _gameManager.SwitchingScene();
        }
    }

    private void Start()
    {
        Video.loopPointReached += showSpaceBar;
    }
    void showSpaceBar(VideoPlayer vp)
    {
        SpaceInstruction.SetActive(true);
        VideoIsEnd=true;
        print("isthere");
    }
}
