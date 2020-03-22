using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    bool _panelActive = false;

    [SerializeField]
    GameObject _infoPanel;

    public void ShowInfoPanel()
    {
        _infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        _infoPanel.SetActive(false);
    }

    public void SwitchStates()
    {
        if(_panelActive == true)
        {
            SoundController.Instance.PlayConfirm();
            HideInfoPanel();
            _panelActive = false;
        }
        else
        {
            SoundController.Instance.PlayConfirm();
            ShowInfoPanel();
            _panelActive = true;
        }
    }
}
