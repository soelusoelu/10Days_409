using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private EnemyCreater enemyCreater;
    private int score;

    private void Start() {
        enemyCreater.OnDeadEnemy(DeadEnemy);
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public int GetScore() {
        return score;
    }

    private void DeadEnemy() {
        AddScore(1);
        Debug.Log("score: " + GetScore());
    }
}
