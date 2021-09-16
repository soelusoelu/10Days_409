using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    public void SetDamage(float amount) {
        damage = amount;
    }

    public float GetDamage() {
        return damage;
    }
}
