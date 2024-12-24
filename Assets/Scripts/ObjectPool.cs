using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;

    List<GameObject> projectiles = new();

    public static ObjectPool Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        for (int i = 0; i < 10; i++) {
            GameObject go = Instantiate(projectilePrefab);
            go.SetActive(false);
            projectiles.Add(go);
        }
    }

    public GameObject GetObject() {
        if (projectiles.Count == 0) {
            CreateProjectile();
        };
        GameObject go = projectiles[0];
        projectiles.RemoveAt(0);
        go.SetActive(true);
        return go;
    }
    private void CreateProjectile() {
        GameObject go = Instantiate(projectilePrefab);
        go.SetActive(false);
        projectiles.Add(go);
    }

    public void ReturnProjectile(GameObject go) {
        go.SetActive(false);
        projectiles.Add(go);
    }

}
