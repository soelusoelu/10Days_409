using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : MonoBehaviour
{
    [SerializeField] private int takeDamageAmount = 10;
    private HitPoint hp;

    private void Start() {
        hp = GetComponent<HitPoint>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.tag.StartsWith("Enemy/")) {
            return;
        }

        hp.TakeDamage(takeDamageAmount);
        Debug.Log("player take damage.");
    }
}
