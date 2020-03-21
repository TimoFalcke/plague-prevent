using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionScreen : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] float moveInDuration = 1f;
    [SerializeField] Ease moveInEase = Ease.OutBounce;
    [SerializeField] float fromHeight = 890;

    [Space]
    [SerializeField] float moveOutDelay = 0.5f;
    [SerializeField] float moveOutDuration = 1f;
    [SerializeField] Ease moveOutEase = Ease.InSine;

    [Space]
    [SerializeField] Ease timeFreezeEase = Ease.OutSine;
    [SerializeField] float timeFreezeDuration = 1;

    [SerializeField] Transform ruleEntryContainer;
    [SerializeField] RuleEntry ruleEntryPrefab;

    List<RuleEntry> shownRuleEntries = new List<RuleEntry>();

    float targetY;

    RectTransform rectTransform;

    private void Awake()
    {
        targetY = transform.localPosition.y;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Display(Rule[] rules)
    {
        ClearRuleEntries();

        for (int i = 0; i < rules.Length; i++)
        {
            CreateRuleEntry(rules[i]);
        }

        gameObject.SetActive(true);


        rectTransform.DOLocalMoveY(targetY, moveInDuration).From(fromHeight).SetEase(moveInEase).SetUpdate(UpdateType.Normal, true);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, timeFreezeDuration).SetUpdate(UpdateType.Normal, true);

    }

    public void Hide()
    {
        for (int i = shownRuleEntries.Count - 1; i >= 0; i--)
        {
            shownRuleEntries[i].Disable();
        }

        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, timeFreezeDuration).SetUpdate(UpdateType.Normal, true);
        rectTransform.DOLocalMoveY(fromHeight, moveInDuration).SetEase(moveOutEase).SetUpdate(UpdateType.Normal, true).SetDelay(moveOutDelay);

    }

    void CreateRuleEntry(Rule rule)
    {
        RuleEntry newEntry = Instantiate(ruleEntryPrefab, ruleEntryContainer);
        newEntry.Initialize(rule);
        shownRuleEntries.Add(newEntry);
    }

    void ClearRuleEntries()
    {
        for (int i = shownRuleEntries.Count - 1; i >= 0; i--)
            Destroy(shownRuleEntries[i].gameObject);

        shownRuleEntries.Clear();
    }
}
