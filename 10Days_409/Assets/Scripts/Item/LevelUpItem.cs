using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 45f;

    private void Update() {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<Level>()?.LevelUp();
            Destroy(gameObject);
        }
    }
}
