using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float explosionTime = 2.5f;
    private Timer explosionTimer;
    public delegate void CallbackOnExplode();
    private CallbackOnExplode onExplode;

    void Start() {
        explosionTimer = new Timer();
        explosionTimer.SetLimitTime(explosionTime);
    }

    void Update() {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, 20f * Time.deltaTime);

        explosionTimer.Update();
        if (explosionTimer.IsTime()) {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.StartsWith("Enemy/")) {
            Explode();
        }
    }

    private void Explode() {
        var c = Instantiate(explosion);
        c.transform.position = transform.position;

        onExplode?.Invoke();

        Destroy(gameObject);
    }

    public void OnExplode(CallbackOnExplode f) {
        onExplode += f;
    }
}
