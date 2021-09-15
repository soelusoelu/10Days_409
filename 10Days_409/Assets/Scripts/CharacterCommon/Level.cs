using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 3;
    public delegate void CallbackOnDead();
    private CallbackOnDead onDead;

    void LevelUp() {
        if (level < maxLevel) {
            ++level;
            Debug.Log("level up.");
        }
    }

    void LevelDown() {
        --level;
        Debug.Log("level down. current level " + level.ToString());

        if (level <= 0) {
            Destroy();
        }
    }

    void OnDead(CallbackOnDead f) {
        onDead += f;
    }

    private void Destroy() {
        Debug.Log("dead.");
        onDead();
    }
}
