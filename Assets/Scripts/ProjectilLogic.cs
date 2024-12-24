using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProjectilLogic : MonoBehaviour, IAttack {
    [SerializeField] private float speed;

    private Vector2 endPoint;
    private Vector2 curVelocity = Vector2.zero;
    private int damage;

    public void Innitiate(List<MyTile> tiles, int damage) {
        this.damage = damage;
        if (tiles.Count == 0 || tiles == null) {
            Debug.LogError("Can't innitiate projectile List is empty");
            ObjectPool.Instance.ReturnProjectile(this.gameObject);
            return;
        }
        transform.position = tiles[0].GetVector3PositionWithOffset();
        endPoint = tiles[tiles.Count-1].GetVector2PositionWithOffset();
        transform.position += Vector3.up * 0.01f;
    }

    private void Move() {
        transform.position = Vector2.SmoothDamp(transform.position, endPoint, ref curVelocity,speed * Time.deltaTime);
        if(Vector2.Distance((Vector2)transform.position,endPoint)<=0.1) {
            ObjectPool.Instance.ReturnProjectile(this.gameObject);
        }
    }
    private void FixedUpdate() { 
    Move();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            GameObject gameObject = collision.gameObject;
            IAttackble attackble = gameObject.GetComponent<IAttackble>();
            if (attackble != null) {
                attackble.TakeDameage(damage);
            }
        }
        
    }

}
