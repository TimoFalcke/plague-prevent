using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentVisual : MonoBehaviour
{
    public enum SpriteStatus { IMUNE, HEALTHY, CARRIER, INFECTED, SEVERE}
    SpriteStatus _spriteStatus = SpriteStatus.HEALTHY;


    Agent _agent;

    [Header("Agent Sprites")]
    [SerializeField]
    Sprite _immunSprite;
    [SerializeField]
    Sprite _healthySprite;
    [SerializeField]
    Sprite _carrierSprite;
    [SerializeField]
    Sprite _infectedSprite;
    [SerializeField]
    Sprite _severeSprite;

    [Header("Fill Sprites")]
    [SerializeField]
    Sprite _carrierFillSprite;
    [SerializeField]
    Sprite _infectedFillSprite;

    private Vector3 _maskMax = new Vector3(0, 0.55f, 0);
    private Vector3 _maskMin = new Vector3(0, 3.78f, 0);

    [Header("Other")]
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    SpriteRenderer _fillRenderer;
    [SerializeField]
    GameObject _mask;

    private void Awake()
    {
        _agent = transform.parent.GetComponent<Agent>();
    }

    private void Update()
    {
        switch (_spriteStatus)
        {
            case SpriteStatus.IMUNE:
                break;
            case SpriteStatus.HEALTHY:
                break;
            case SpriteStatus.CARRIER:
                int ovr = _agent.InfectionStateMachine.infection_overall +
                          _agent.InfectionStateMachine.recovery_overall;
                _mask.transform.localPosition = _maskMax * ovr/13 ;
                break;
            case SpriteStatus.INFECTED:
                _mask.transform.localPosition = _maskMax * _agent.InfectionStateMachine.disease_overall / 13;
                break;
            case SpriteStatus.SEVERE:
                break;
        }
    }

    public void SetState(SpriteStatus spriteStatus)
    {
        _spriteStatus = spriteStatus;

        switch (_spriteStatus)
        {
            case SpriteStatus.IMUNE:
                _spriteRenderer.sprite = _immunSprite;
                _fillRenderer.gameObject.SetActive(false);
                break;
            case SpriteStatus.HEALTHY:
                _spriteRenderer.sprite = _healthySprite;
                _fillRenderer.gameObject.SetActive(false);
                break;
            case SpriteStatus.CARRIER:
                _spriteRenderer.sprite = _carrierSprite;
                _fillRenderer.sprite = _carrierFillSprite;
                _mask.transform.localPosition = _maskMin;
                _fillRenderer.gameObject.SetActive(true);
                break;
            case SpriteStatus.INFECTED:
                _spriteRenderer.sprite = _infectedSprite;
                _fillRenderer.sprite = _infectedFillSprite;
                _mask.transform.localPosition = _maskMin;
                _fillRenderer.gameObject.SetActive(true);
                break;
            case SpriteStatus.SEVERE:
                _spriteRenderer.sprite = _severeSprite;
                _fillRenderer.gameObject.SetActive(false);
                break;
        }
    }
}
