using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonTweener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    //[SerializeField] float hoverScale = 1.05f;
    [SerializeField] float hoverAnimDuration = 0.1f;

    [SerializeField] Vector2 hoverSizeDelta = new Vector2(0, 10f);
    [SerializeField] Vector2 pressedSizeDelta = new Vector2(0, -10f);

    //[SerializeField] float pressedScale = 0.95f;
    [SerializeField] float pressedAnimDuration = 0.1f;

    [SerializeField] TextMeshProUGUI[] hoverColorTexts;
    [SerializeField] Color hoverColor;

    Vector3 originalScale;
    Vector2 originalRectSize;

    RectTransform rectTransform;

    private void Awake()
    {
        originalScale = transform.localScale;

        rectTransform = GetComponent<RectTransform>();
        originalRectSize = rectTransform.sizeDelta;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rectTransform.DOSizeDelta(originalRectSize, pressedAnimDuration).SetUpdate(UpdateType.Normal, true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.DOSizeDelta(originalRectSize + pressedSizeDelta, pressedAnimDuration).SetUpdate(UpdateType.Normal, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOSizeDelta(originalRectSize + hoverSizeDelta, hoverAnimDuration).SetUpdate(UpdateType.Normal, true);
        foreach(TextMeshProUGUI hoverColorText in hoverColorTexts)
        {
            hoverColorText.DOColor(hoverColor, hoverAnimDuration).SetUpdate(UpdateType.Normal, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOSizeDelta(originalRectSize, hoverAnimDuration).SetUpdate(UpdateType.Normal, true);
        foreach (TextMeshProUGUI hoverColorText in hoverColorTexts)
        {
            hoverColorText.DOColor(Color.white, hoverAnimDuration).SetUpdate(UpdateType.Normal, true);
        }
    }
}
