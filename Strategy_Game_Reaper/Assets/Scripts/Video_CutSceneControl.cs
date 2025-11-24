using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_CutSceneControl : MonoBehaviour
{
    public GameObject SpaceInstruction;
    public VideoPlayer Video;

    private void Start()
    {
        Video.loopPointReached += showSpaceBar;
    }
    void showSpaceBar(VideoPlayer vp)
    {
        SpaceInstruction.SetActive(true);
        print("isthere");
    }
}
