using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 120f;
    [SerializeField] private float destroyTime = 5f;
    private Timer destroyTimer;

    void Start() {
        destroyTimer = new Timer();
        destroyTimer.SetLimitTime(destroyTime);
    }

    void Update() {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        destroyTimer.Update();
        if (destroyTimer.IsTime()) {
            Destroy(gameObject);
        }
    }
}
