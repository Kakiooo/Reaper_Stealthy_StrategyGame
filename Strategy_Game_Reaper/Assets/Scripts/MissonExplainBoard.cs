using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonExplainBoard : MonoBehaviour
{
    public RectTransform MissionBoard;
    public RectTransform PivotPoint;

    private void Awake()
    {
        MissionBoard.DOAnchorPos(PivotPoint.anchoredPosition, 0.5f);
    }
}
