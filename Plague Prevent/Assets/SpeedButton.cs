using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    private enum GameSpeed { ONE, DOUBLE, FIFTH}
    private GameSpeed _gameSpeed;

    [SerializeField]
    Sprite _arrow1;
    [SerializeField]
    Sprite _arrow2;
    [SerializeField]
    Sprite _arrow3;

    Image _buttonImage;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void ChangeSpeed()
    {
        switch (_gameSpeed)
        {
            case GameSpeed.ONE:
                Time.timeScale = 2;
                SoundController.Instance.PlaySpeed2();
                _buttonImage.sprite = _arrow2;
                _gameSpeed = GameSpeed.DOUBLE;
                break;
            case GameSpeed.DOUBLE:
                Time.timeScale = 4;
                SoundController.Instance.PlaySpeed3();
                _buttonImage.sprite = _arrow3;
                _gameSpeed = GameSpeed.FIFTH;
                break;
            case GameSpeed.FIFTH:
                Time.timeScale = 1;
                SoundController.Instance.PlaySpeed1();
                _buttonImage.sprite = _arrow1;
                _gameSpeed = GameSpeed.ONE;
                break;

        }

    }
}
