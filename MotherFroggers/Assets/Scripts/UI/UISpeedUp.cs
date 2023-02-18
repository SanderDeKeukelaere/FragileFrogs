using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpeedUp : MonoBehaviour
{
    private bool _speedUp = false;

    [SerializeField] Image _arrowImage;

    public void OnClick()
    {
        _speedUp = !_speedUp;
        if (_speedUp)
        {
            Time.timeScale = 1.5f;
            _arrowImage.color = Color.yellow;
        }
        else
        {
            Time.timeScale = 1;
            _arrowImage.color = Color.white;
        }
    }
}
