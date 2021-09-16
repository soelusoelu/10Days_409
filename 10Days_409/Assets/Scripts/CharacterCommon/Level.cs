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

    public void LevelUp() {
        if (level < maxLevel) {
            ++level;
            Debug.Log("level up. current level " + level.ToString());

            if (onUpdateLevel != null) {
                onUpdateLevel();
            }
        }
    }

    public void LevelDown() {
        --level;
        Debug.Log("level down. current level " + level.ToString());

        if (onUpdateLevel != null) {
            onUpdateLevel();
        }

        if (level <= 0) {
            Dead();
        }
    }

    public int GetLevel() {
        return level;
    }

    public int GetMaxLevel() {
        return maxLevel;
    }

    public void OnUpdateLevel(CallbackOnUpdateLevel f) {
        onUpdateLevel += f;
    }

    public void OnDead(CallbackOnDead f) {
        onDead += f;
    }

    private void Dead() {
        Debug.Log("dead.");
        if (onDead != null) {
            onDead();
        }
    }
}
