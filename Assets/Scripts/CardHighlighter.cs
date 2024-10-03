using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHighlighter : MonoBehaviour {

    [SerializeField] HorizontalLayoutGroup horizontalLayoutGroup; 
    private float yOffSet = 30;
    private RectTransform selectedCard;
    private int originalSiblingIndex;
    private Coroutine coroutine;

    private void Start() {
        CardManager.OnHovered += OnHovered_HigihlightTheCard;
        CardManager.OnUnselected += CardManager_OnUnselected;
    }

    private void OnDestroy() {
        CardManager.OnHovered -= OnHovered_HigihlightTheCard;
        CardManager.OnUnselected -= CardManager_OnUnselected;
    }

    private void CardManager_OnUnselected() {
        if (selectedCard != null) {
            RemoveHighlight();
        }
    }

    public void OnHovered_HigihlightTheCard(Card card) {
        RectTransform rectTransform = card.GetRectTransform();
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        if (selectedCard != null) {
            RemoveHighlight();
        }
        selectedCard = rectTransform;
        originalSiblingIndex = selectedCard.GetSiblingIndex();
        coroutine = StartCoroutine(HighlightTheCard(0.05f));

    }

    private IEnumerator HighlightTheCard(float time) {
        yield return new WaitForSeconds(time);
        horizontalLayoutGroup.enabled = false;
        selectedCard.transform.position = new Vector3(selectedCard.transform.position.x, selectedCard.position.y + yOffSet, 0);
        selectedCard.SetAsLastSibling();
    }

    private void RemoveHighlight() {
        horizontalLayoutGroup.enabled = true;
        selectedCard.SetSiblingIndex(originalSiblingIndex);
    }
}
