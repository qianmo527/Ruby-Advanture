using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    void Update() {
        // 对子弹发射距离的检测和销毁
        if (transform.position.magnitude > 100) {
            Destroy(gameObject);
        }
    }

    // 发射方法
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction*force);
    }

    // 子弹的检测碰撞方法
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
        if (enemyController!=null){
            enemyController.Fix();
        }
        Destroy(gameObject);
    }
}
