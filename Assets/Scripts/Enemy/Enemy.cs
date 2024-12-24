using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour, IAttackble {

    public static event Action OnEnergyZero;
    public static event Action OnDie;

    public EnemySO so;
    public EnemyAwarnes awarnes;
    public EnemyMovement movement;

    private int hp;
    private int maxEnergy;
    private int currentEnergy = 0;
    private MyGrid grid;

    private bool isActive = false;
    private bool isReadyToMakeAction = true;


    private void Start() {
        grid = MyGrid.Instance;
        hp = so.hp;
        maxEnergy = so.energy;
        TurnManager.OnEnemyTurnStart += ResetEnergy;
        StartCoroutine(AwaitGrid());
    }

    private IEnumerator AwaitGrid() {
        while (!grid.isTilesGenerated) {
            yield return null;
        }
        movement.Initiate(grid, this);
        awarnes.Initiate(grid, so);
    }

    private void OnDestroy() {
        TurnManager.OnEnemyTurnStart -= ResetEnergy;
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

    public void SpendEnergy(int e) {
        currentEnergy -= e;
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

    public void Wait() {
        isReadyToMakeAction = false;
        StartCoroutine(ResetIsReadyToMakeAction());
    }

    private IEnumerator ResetIsReadyToMakeAction() {
        yield return new WaitForSeconds(1f);
        isReadyToMakeAction = true;
    }

    public bool IsReadyToMakeAction() {
        return isReadyToMakeAction;
    }

    public bool isEnoughEnergy(int e) {
        return e <= currentEnergy;
    }
    public bool IsActive() {
        return isActive;
    }

    public void TakeDameage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            Die();
        }
    }

    private void Die() {
        OnDie?.Invoke();
        gameObject.SetActive(false);
        Destroy(this);
    }
}
