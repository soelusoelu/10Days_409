using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 3;
    public delegate void CallbackOnDead();
    private CallbackOnDead onDead;
    public delegate void CallbackOnUpdateLevel();
    private CallbackOnUpdateLevel onUpdateLevel;

    void LevelUp() {
        if (level < maxLevel) {
            ++level;
            Debug.Log("level up.");

            onUpdateLevel();
        }
    }

    void LevelDown() {
        --level;
        Debug.Log("level down. current level " + level.ToString());

        onUpdateLevel();

        if (level <= 0) {
            Destroy();
        }
    }

    int GetLevel() {
        return level;
    }

    int GetMaxLevel() {
        return maxLevel;
    }

    void OnUpdateLevel(CallbackOnUpdateLevel f) {
        onUpdateLevel += f;
    }

    void OnDead(CallbackOnDead f) {
        onDead += f;
    }

    private void Destroy() {
        Debug.Log("dead.");
        onDead();
    }
}
