using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 120f;
    [SerializeField] private float destroyTime = 5f;
    private Timer timer;

    void Start() {
        timer = new Timer();
        timer.SetLimitTime(destroyTime);
    }

    void Update() {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        timer.Update();
        if (timer.IsTime()) {
            Destroy(gameObject);
        }
    }
}
