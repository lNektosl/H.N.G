using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

    [SerializeField] protected CardSO cardSO;
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected CardDisplay cardDisplay;

    public static event Action<Card> OnCardEnter;
    public static event Action OnCardExit;
    public static event Action<Card> OnCardDraged;
    public static event Action OnCardDrop;

    private int originalSiblingIndex;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;



    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        originalSiblingIndex = rectTransform.GetSiblingIndex();
        AddToStart();
    }

    protected virtual void AddToStart() { }
    public void OnPointerEnter(PointerEventData eventData) {
        if (isDragging)
            return;
        OnCardEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isDragging)
            return;
        OnCardExit?.Invoke();
    }


    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;

    }

    public void OnBeginDrag(PointerEventData eventData) {
        OnCardDraged?.Invoke(this);
        canvasGroup.blocksRaycasts = false;
        rectTransform.SetAsLastSibling();
        isDragging = true;

    }

    public void OnEndDrag(PointerEventData eventData) {
        OnCardDrop?.Invoke();
        canvasGroup.blocksRaycasts = true;
        rectTransform.SetSiblingIndex(originalSiblingIndex);
        isDragging = false;

    }

    public CardSO GetCardSO() {
        return cardSO;
    }

    public virtual void Use() {
        Deactivate();
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void SetParent(Transform parent) {
        rectTransform.SetParent(parent, false);
    }

    public void SetCartSO(CardSO cardSO) {
        this.cardSO = cardSO;
        UpdateVisuale();
    }

    private void UpdateVisuale() {
        cardDisplay.Initialize(cardSO);
    }

    public RectTransform GetRectTransform() {
        return rectTransform;
    }

}
