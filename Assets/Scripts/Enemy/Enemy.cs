using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour {
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float awarnesRange;

    public static event Action OnEnergyZero;

    private int maxEnergy = 2;
    private int currentEnergy = 0;
    private float checksFrequency = 0.5f;

    private Vector2 lastSeenPlayerPosition;

    private bool isActive = false;
    private bool isReadyToMakeAction = true;
    private bool isPlayerInSight = false;
    private bool isAwareOfThePlayer = false;
    private bool isTimeToCheckForPlayer = true;

    private void Start() {
        TurnManager.OnEnemyTurnStart += ResetEnergy;
    }

    private void OnDestroy() {
        TurnManager.OnEnemyTurnStart -= ResetEnergy;
    }

    private void FixedUpdate() {
        if (isTimeToCheckForPlayer) {
            CheckIfPlayerInAwarenesRange();
        }
    }

    public void SpendEnergy() {
        currentEnergy--;
        if (currentEnergy == 0) {
            OnEnergyZero?.Invoke();
            isActive = false;
        }
        isReadyToMakeAction = false;
        StartCoroutine(ResetIsReadyToMakeAction());
    }

    private void ResetEnergy() {
        currentEnergy = maxEnergy;
        isActive = true;
    }

    private IEnumerator ResetIsReadyToMakeAction() {
        yield return new WaitForSeconds(1f);
        isReadyToMakeAction = true;
    }

    private void CheckIfPlayerInAwarenesRange() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5, playerLayerMask);
        foreach (Collider2D collider in colliders) {
            CheckIfCanSeePlayer(collider);
        }

        isTimeToCheckForPlayer = false;
        StartCoroutine(ResetIsTimeToCheckForPlayer());
    }

    private void CheckIfCanSeePlayer(Collider2D collider) {
        Vector2 direction = (collider.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, awarnesRange, ~mask);

        if (hit.collider != null && hit.collider.CompareTag("Player")) {
            isPlayerInSight = true;
            isAwareOfThePlayer = true;
            lastSeenPlayerPosition = hit.collider.transform.position;
            Debug.Log(lastSeenPlayerPosition);
        } else {
            isPlayerInSight = false;
        }
    }

    private IEnumerator ResetIsTimeToCheckForPlayer() {
        yield return new WaitForSeconds(checksFrequency);
        isTimeToCheckForPlayer = true;
    }

    public bool IsReadyToMakeAction() {
        return isReadyToMakeAction;
    }

    public bool IsActive() {
        return isActive;
    }

    public bool IsPlayerInSight() {
        return isPlayerInSight;
    }

    public bool IsAwareOfThePlayer() {
        return isAwareOfThePlayer;
    }

    public void ResetIsAwareOfThePlater() {
        isAwareOfThePlayer = false;
    }

}
