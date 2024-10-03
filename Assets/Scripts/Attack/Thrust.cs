using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Thrust : MonoBehaviour, IAttack {
    [SerializeField] private float speed;

    private Collider2D coll2D;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 curVelocity = Vector2.zero;



    private void Awake() {
        coll2D = GetComponent<Collider2D>();
    }

    public void Innitiate(List<Vector2> tiles) {
        if (tiles.Count == 0 || tiles == null) {
            Debug.LogError("Can't innitiate projectile List is empty");
            Destroy(this.gameObject);
            return;
        }
        endPoint = tiles[tiles.Count - 1];
        direction = (endPoint - (Vector2)transform.position).normalized;

        Rotate();
    }

    private void Move() {

        transform.position = Vector2.SmoothDamp(transform.position, endPoint,ref curVelocity, speed * Time.deltaTime);
        if (Vector2.Distance((Vector2)transform.position, endPoint) <= 0.1) {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate() {
        Move();
    }

    private void Rotate() {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -90));
    }
}
