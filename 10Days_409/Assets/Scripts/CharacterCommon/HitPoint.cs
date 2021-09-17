using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHp = 100;
    public delegate void CallbackOnUpdateHp(HitPoint hp);
    private CallbackOnUpdateHp onUpdateHp;

    public void TakeDamage(int amount) {
        hp -= amount;
        if (hp < 0) {
            hp = 0;
        }

        onUpdateHp?.Invoke(this);
    }

    public void TakeHeal(int amount) {
        hp += amount;
        if (hp > maxHp) {
            hp = maxHp;
        }

        onUpdateHp?.Invoke(this);
    }

    public void SetHp(int amount) {
        hp = amount;
        maxHp = amount;
    }

    public int GetHp() {
        return hp;
    }

    public int GetMaxHp() {
        return maxHp;
    }

    public void OnUpdateHp(CallbackOnUpdateHp f) {
        onUpdateHp += f;
    }
}
