using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // Update is called once per frame
    public void Pause()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 1).SetUpdate(UpdateType.Normal, true);
    }

    public void Unpause()
    {
        SoundController.Instance.PlayConfirm();
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 1).SetUpdate(UpdateType.Normal, true);
    }

    public void RestartLevel()
    {
        SoundController.Instance.PlayConfirm();
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0).SetUpdate(UpdateType.Normal, true);
        SceneManager.LoadScene(0);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
