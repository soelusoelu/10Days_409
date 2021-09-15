using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.position += Vector3.right * h * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * v * moveSpeed * Time.deltaTime;
    }
}
