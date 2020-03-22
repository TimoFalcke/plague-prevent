using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Controller
{
    #region fields
    private static SoundController _instance;

    [SerializeField]
    AudioClip _agentDeath;

    [SerializeField]
    AudioClip _agentInfected1;

    [SerializeField]
    AudioClip _agentInfected2;

    [SerializeField]
    AudioClip _confirm;

    [SerializeField]
    AudioClip _hoverSound;

    [SerializeField]
    AudioClip _speed1;

    [SerializeField]
    AudioClip _speed2;

    [SerializeField]
    AudioClip _speed3;

    [SerializeField]
    AudioSource _audioSource;

    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public SoundController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }
    #endregion

    public void PlayAgentDeath()
    {
        _audioSource.clip = _agentDeath;
        _audioSource.Play();
    }

    public void PlayAgentInfected1()
    {
        _audioSource.clip = _agentInfected1;
        _audioSource.Play();
    }

    public void PlayAgentInfected2()
    {
        _audioSource.clip = _agentInfected2;
        _audioSource.Play();
    }
    public void PlaySpeed1()
    {
        _audioSource.clip = _speed1;
        _audioSource.Play();
    }
    public void PlaySpeed2()
    {
        _audioSource.clip = _speed2;
        _audioSource.Play();
    }
    public void PlaySpeed3()
    {
        _audioSource.clip = _speed3;
        _audioSource.Play();
    }

    public void PlayConfirm()
    {
        _audioSource.clip = _confirm;
        _audioSource.Play();
    }


    #region properties
    public static SoundController Instance { get => _instance; }
    #endregion

}
