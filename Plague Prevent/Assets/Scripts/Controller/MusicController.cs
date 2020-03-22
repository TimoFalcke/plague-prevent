using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicController : MonoBehaviour
{
    #region fields
    enum MusicLayer { Layer1, Layer2, Layer3};
    MusicLayer _musicLayer = MusicLayer.Layer1;
    private static MusicController _instance;
    [SerializeField]
    AudioSource _audioSource1;
    [SerializeField]
    AudioSource _audioSource2;
    [SerializeField]
    AudioSource _audioSource3;

    float t1 = 1;
    float t2 = 1;
    float t3 = 1;
    float transitionTime = 100f;
    float volume = 0;

    bool _volumeActive = false;
    bool _muted = true;
#endregion

    #region initilization
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GetInstance();
        StartMusic();
    }


    private void Update()
    {
        if (_muted == false)
        {
            switch (_musicLayer)
            {
                case MusicLayer.Layer1:
                    t1 += Time.deltaTime / transitionTime;
                    _audioSource1.volume = Mathf.Lerp(_audioSource1.volume, volume, t1);
                    t2 += Time.deltaTime / transitionTime;
                    _audioSource2.volume = Mathf.Lerp(_audioSource2.volume, 0, t2);
                    t3 += Time.deltaTime / transitionTime;
                    _audioSource3.volume = Mathf.Lerp(_audioSource3.volume, 0, t3);
                    break;
                case MusicLayer.Layer2:
                    t1 += Time.deltaTime / transitionTime;
                    _audioSource1.volume = Mathf.Lerp(_audioSource1.volume, 0, t1);
                    t2 += Time.deltaTime / transitionTime;
                    _audioSource2.volume = Mathf.Lerp(_audioSource2.volume, volume, t2);
                    t3 += Time.deltaTime / transitionTime;
                    _audioSource3.volume = Mathf.Lerp(_audioSource3.volume, 0, t3);
                    break;
                case MusicLayer.Layer3:
                    t1 += Time.deltaTime / transitionTime;
                    _audioSource1.volume = Mathf.Lerp(_audioSource1.volume, 0, t1);
                    t2 += Time.deltaTime / transitionTime;
                    _audioSource2.volume = Mathf.Lerp(_audioSource2.volume, 0, t2);
                    t3 += Time.deltaTime / transitionTime;
                    _audioSource3.volume = Mathf.Lerp(_audioSource3.volume, volume, t3);
                    break;
            }
        }
    }

    public void SwtitchToLayer1()
    {
        t1 = 0;
        t2 = 0;
        t3 = 0;
        _musicLayer = MusicLayer.Layer1;
        //_audioSource1.volume = 1;
        //_audioSource2.volume = 0;
       // _audioSource3.volume = 0;
    }
    public void SwtitchToLayer2()
    {
        t1 = 0;
        t2 = 0;
        t3 = 0;
        _musicLayer = MusicLayer.Layer2;
        //_audioSource1.volume = 0;
        //_audioSource2.volume = 1;
        //_audioSource3.volume = 0;
    }
    public void SwtitchToLayer3()
    {
        t1 = 0;
        t2 = 0;
        t3 = 0;
        _musicLayer = MusicLayer.Layer3;
    }
    public void Mute()
    {
        t1 += Time.deltaTime / transitionTime;
        _audioSource1.volume = Mathf.Lerp(_audioSource1.volume, 0, t1);
        t2 += Time.deltaTime / transitionTime;
        _audioSource2.volume = Mathf.Lerp(_audioSource2.volume, 0, t2);
        t3 += Time.deltaTime / transitionTime;
        _audioSource3.volume = Mathf.Lerp(_audioSource3.volume, 0, t3);
        _muted = true;
    }

    public void UnMute()
    {
       
        _muted = false;
    }

    public void StartMusic()
    {
        StartCoroutine(StartMusicRoutine(1f, 5));

    }


    IEnumerator StartMusicRoutine(float delay, float time) {

        yield return new WaitForSeconds(delay);
        volume = 0.1f;
        while (volume < 1)
        {
            volume += 1 * Time.deltaTime/ time;
            if(_muted == true)
            {
                switch (_musicLayer)
                {
                    case MusicLayer.Layer1:
                        _audioSource1.volume = volume;
                        break;
                    case MusicLayer.Layer2:
                        _audioSource2.volume = volume;
                        break;
                    case MusicLayer.Layer3:
                        _audioSource3.volume = volume;
                        break;
                }
                _audioSource1.Play();
                _audioSource2.Play();
                _audioSource3.Play();
                _audioSource1.loop = true;
                _audioSource2.loop = true;
                _audioSource3.loop = true;
                _muted = false;
            }
            yield return null;
        }
    }
    #endregion

 


    #region methods
    public MusicController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }
    #endregion

    #region properties
    public static MusicController Instance { get => _instance; }
    #endregion

}
