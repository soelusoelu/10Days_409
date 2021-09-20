using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : MonoBehaviour
{
    [SerializeField] private int takeDamageAmount = 10;
    private HitPoint hp;
    private Level level;

    private void Start() {
        hp = GetComponent<HitPoint>();
        level = GetComponent<Level>();
        level.OnDead(Dead);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.tag.StartsWith("Enemy/")) {
            return;
        }

        hp.TakeDamage(takeDamageAmount);
        if (hp.GetHp() <= 0) {
            level.LevelDown();

            if (level.GetLevel() != 0) {
                hp.ResetHp();
            }
        }
    }

    private void Dead() {
        Destroy(gameObject);
    }
}
