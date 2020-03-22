using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField]
    Image _blackScreen;

    float _instensity = 1;
    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator StartMusicRoutine(float delay, float time)
    {
        yield return new WaitForSeconds(delay);
        _instensity = 1f;
        while (_instensity < 1)
        {
            
            yield return null;
        }
    }
}
