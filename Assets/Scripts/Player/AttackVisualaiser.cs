using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackVisualaiser : MonoBehaviour {


    [SerializeField] private GameObject highlightTile;

    private Player player;
    private IList<Vector2> tilesList = new List<Vector2>();
    private IList<GameObject> visualisedTiles = new List<GameObject>();
    private Card selectedCard;
    private Vector2 initialPosition;
    private Vector2 direction;

    private void Start() {
        player = Player.Instance;

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
        UpdateVisualisation();
    }

    private void Card_OnUnhovered() {
        RemoveVisualistion();
        selectedCard = null;
    }

    private void UpdateVisualisation() {
        GetCardTiles();
        RemoveVisualistion();
        VisualiseTiles();
    }

    private void GetCardTiles() {
        if (selectedCard is AttackCard attackCard) {
            RecalculateInitialPosition();
            tilesList = attackCard.GetTiles(initialPosition, direction);
        }
    }

    private void VisualiseTiles() {
        foreach (var tilePos in tilesList) {
            visualisedTiles.Add(Instantiate(highlightTile, tilePos, Quaternion.identity));
        }
    }

    private void RemoveVisualistion() {
        foreach (var tile in visualisedTiles) {
            Destroy(tile);
        }
        visualisedTiles.Clear();
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

}
