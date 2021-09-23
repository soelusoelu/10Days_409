using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int rushEnemyScore = 100;
    [SerializeField] private int blockEnemyScore = 150;
    [SerializeField] private int bossScore = 1000;
    [SerializeField] private float scoreAddTime = 0.5f;
    [SerializeField] private int addTimeScore = 1;
    private Timer timer;
    private int score;
    private bool isUpdate = true;

    private void Start() {
        timer = new Timer();
        timer.SetLimitTime(scoreAddTime);
    }

    private void Update() {
        if (!isUpdate) {
            return;
        }

        timer.Update();
        if (timer.IsTime()) {
            timer.Reset();
            AddScore(addTimeScore);
        }
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public void AddScore(string tag, float posZ) {
        if (posZ < -1f) {
            return;
        }

        int score = 0;
        if (tag == "Enemy/RushEnemy") {
            score = rushEnemyScore;
        } else if (tag == "Enemy/BlockEnemy") {
            score = blockEnemyScore;
        } else if (tag == "Enemy/Boss") {
            score = bossScore;
        }

        AddScore(score);
    }

    public int GetScore() {
        return score;
    }

    public void DisableUpdate() {
        isUpdate = false;
    }
}
