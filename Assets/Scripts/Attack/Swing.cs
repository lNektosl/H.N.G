using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Swing : MonoBehaviour, IAttack {
    [SerializeField] private float speed;

    private Collider2D coll2D;
    private Vector2 endPoint;
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
        endPoint = tiles[tiles.Count-1];
    }

    private void Move() {
        transform.position = Vector2.SmoothDamp(transform.position, endPoint, ref curVelocity,speed * Time.deltaTime);
        if(Vector2.Distance((Vector2)transform.position,endPoint)<=0.1) {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate() { 
    Move();
    }
}
