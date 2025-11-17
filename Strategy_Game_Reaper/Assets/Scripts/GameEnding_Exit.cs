using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding_Exit : MonoBehaviour
{
    public Ending_DisplayResult Ending_Result;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Ending_Result.ShownResultEnd_Menu = true;
        }
    }
}
