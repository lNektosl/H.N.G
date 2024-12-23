using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAwarnes : MonoBehaviour {

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask ignoreMask;

    private float awarnes;
    private float awarnesModifier = 1f;

    private float checksFrequency = 0.5f;
    private bool isTimeToCheckForPlayer = true;

    private MyGrid grid;
    private MyTile playerPosition;
    private bool isAwareOfPlayer = false;
    private bool isPlayerVisible = false;

    public void Initiate(MyGrid grid, EnemySO enemy) {
        this.grid = grid;
        awarnes = enemy.baseAwarnes;
    }
    private void FixedUpdate() {
        if (isTimeToCheckForPlayer) {
            CheckPlayerAwareness();
        }
    }
    private void CheckPlayerAwareness() {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, GetAwarnesRange(), playerLayerMask);
        if (collider != null) {
            CheckIfCanSeePlayer(collider);
        } else {
            isPlayerVisible = false;
        }

        isTimeToCheckForPlayer = false;
        StartCoroutine(ResetIsTimeToCheckForPlayer());
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetAwarnesRange());
    }
    private void CheckIfCanSeePlayer(Collider2D collider) {
        Vector2 direction = (collider.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, GetAwarnesRange(), ~ignoreMask);

        if (hit.collider != null && hit.collider.CompareTag("Player")) {
            isAwareOfPlayer = true;
            isPlayerVisible = true;
            playerPosition = grid.GetTile(hit.collider.transform.position);
        } else {
            isPlayerVisible = false;
        }
    }

    private IEnumerator ResetIsTimeToCheckForPlayer() {
        yield return new WaitForSeconds(checksFrequency);
        isTimeToCheckForPlayer = true;
    }

    private float GetAwarnesRange() {
        return awarnes * awarnesModifier;
    }

    public MyTile GetPlayerPosition() {
        return playerPosition;
    }

    public bool IsAwareOfPlayer() {
        return isAwareOfPlayer;
    }
    public bool IsPlayerVisible() {
        return isPlayerVisible;
    }
    public void SetAwarnesModifier(float f) {
        awarnesModifier = f;
    }

    public void ResetAwareness() {
        isAwareOfPlayer = false;
        isPlayerVisible = false;
        playerPosition = null;
        awarnesModifier = 1f;
    }

}
