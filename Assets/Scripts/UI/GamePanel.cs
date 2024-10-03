using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanel : MonoBehaviour, IDropHandler, IPointerClickHandler {
    public static event Action OnDropOrClickRegistered;

    private Card card;
    

    public void OnDrop(PointerEventData eventData) {
        OnDropOrClickRegistered?.Invoke();

    }

    public void OnPointerClick(PointerEventData eventData) {
        OnDropOrClickRegistered?.Invoke();
    }
}
