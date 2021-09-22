using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int rushEnemyScore = 100;
    [SerializeField] private int blockEnemyScore = 150;
    [SerializeField] private int bossScore = 1000;
    private int score;

    private void Start() {
        EnemyDestroyer.OnDestroyEnemy(DeadEnemy);
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public int GetScore() {
        return score;
    }

    private void DeadEnemy(GameObject enemy, bool outOfArea) {
        if (outOfArea) {
            return;
        }

        int score = 0;
        if (enemy.tag == "Enemy/RushEnemy") {
            score = rushEnemyScore;
        } else if (enemy.tag == "Enemy/BlockEnemy") {
            score = blockEnemyScore;
        } else if (enemy.tag == "Enemy/Boss") {
            score = bossScore;
        }

        AddScore(score);
    }
}
