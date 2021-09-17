using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public void SetDamage(int amount) {
        damage = amount;
    }

    public int GetDamage() {
        return damage;
    }
}
