using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(StarGameCoroutine());

    }

    IEnumerator StarGameCoroutine()
    {
        SoundController.Instance.PlayConfirm();
        yield return new WaitForSeconds(0.75f);

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
