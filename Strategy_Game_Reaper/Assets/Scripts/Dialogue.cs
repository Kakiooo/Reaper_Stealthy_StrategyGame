using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _potato;
    [SerializeField] TextMeshProUGUI _tomato;
    [SerializeField] GameObject _potatoDialogueGroup;
    [SerializeField] GameObject _tomatoDialogueGroup;
    [SerializeField] float _intervalTime;
    [SerializeField] int _index;
    [SerializeField] List<string> _line=new List<string>();

    private void Start()
    {
        StartCoroutine("ShowText", _potato);
       // _potatoDialogueGroup.SetActive(false);
        _tomatoDialogueGroup.SetActive(false);

    }
    private void Update()
    {
        SwitchLine();
    }


    IEnumerator  ShowText(TextMeshProUGUI whoSpeaking)
    {
        whoSpeaking.text = "";
        foreach (char character in _line[_index].ToCharArray())
        {
            whoSpeaking.text += character;
            yield return new WaitForSeconds(_intervalTime);
        }
    }

    void SwitchLine()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _index++;
            if (_index >= _line.Count) return;
            print("CAlled");
            if (_index % 2 == 0)
            {
                _potatoDialogueGroup.SetActive(false);
                _tomatoDialogueGroup.SetActive(true);
                StartCoroutine("ShowText", _tomato);
            }
            else if (_index % 2 !=0)
            {
                _potatoDialogueGroup.SetActive(true);
                _tomatoDialogueGroup.SetActive(false);
                StartCoroutine("ShowText", _potato);
            }

        }
    }
}
