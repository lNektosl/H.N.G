using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class AttackVisualaiser : MonoBehaviour {

    private Player player;
    private MyGrid grid;
    private IList<MyTile> tilesList = new List<MyTile>();
    private Card selectedCard;
    private Vector2 lastPosition;
    private Vector2 initialPosition;
    private Vector2 direction;

    private void Start() {
        player = Player.Instance;
        grid = MyGrid.Instance;

        CardManager.OnSelected += Card_OnSelected;
        CardManager.OnUnselected += Card_OnUnhovered;
    }

    private void OnDestroy() {
        CardManager.OnSelected -= Card_OnSelected;
        CardManager.OnUnselected -= Card_OnUnhovered;
    }

    private void Update() {
        if (selectedCard != null) {
            UpdateVisualisation();
        }
    }


    private void Card_OnSelected(Card card) {
        selectedCard = card;
        lastPosition = Vector2.zero;
        UpdateVisualisation();
    }

    private void Card_OnUnhovered() {
        RemoveVisualistion();
        selectedCard = null;
    }

    private void UpdateVisualisation() {
        GetCardTiles();
        VisualiseTiles();
    }

    private void GetCardTiles() {
        RecalculateInitialPosition();
        if (selectedCard is AttackCard attackCard && lastPosition != initialPosition) {
            RemoveVisualistion();
            tilesList = attackCard.GetTiles(initialPosition, direction, grid);
            SetLastPosition();
        }
    }

    private void VisualiseTiles() {
        foreach (var tile in tilesList) {
            if (tile != null)
                tile.SetColor(Color.red);
        }
    }

    private void RemoveVisualistion() {
        foreach (var tile in tilesList) {
            if (tile != null)
                tile.ResetColor();
        }
        tilesList.Clear();
    }


    private void RecalculateInitialPosition() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = player.transform.position;
        direction = (mousePosition - playerPosition).normalized;

        if (Mathf.Abs(direction.x) <= Mathf.Abs(direction.y)) {
            initialPosition = new(player.transform.position.x,
                player.transform.position.y + (direction.y >= 0.5 ? 1 : -1));
        }

        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)) {
            initialPosition = new(player.transform.position.x + (direction.x >= 0.5 ? 1 : -1),
                player.transform.position.y);

        }
    }

    private void SetLastPosition() {
        lastPosition = initialPosition;
    }
}
