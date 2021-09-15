using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 120f;

    void Update() {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
