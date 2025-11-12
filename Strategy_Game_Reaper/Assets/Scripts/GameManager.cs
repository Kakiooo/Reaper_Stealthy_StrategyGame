using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool LoseLevel;

    private void Update()
    {
        LoseResult();
    }

    void LoseResult()
    {
        if (LoseLevel)
        print("Sorry You Lose");
    }
}
