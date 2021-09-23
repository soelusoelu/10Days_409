using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int rushEnemyScore = 100;
    [SerializeField] private int blockEnemyScore = 150;
    [SerializeField] private int bossScore = 1000;
    private int score;
    private int _ScorePool;

    private bool isUpdate = true;

    private void Start() 
    {
        _ScorePool = 0;
    }

    private void Update()
    {

        UpdateScore();

        if (!isUpdate) {
            return;
        }

    }

    void UpdateScore()
    {
        if (_ScorePool <= 0)
        {
            return;
        }

        _ScorePool -= 1;
        score += 1;
    }


    public void AddScore(int amount) {
        _ScorePool += amount;
    }

    void AdjustmentScore()
    {
        score += _ScorePool;
        _ScorePool = 0;
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

    public void DisableUpdate()
    {
        AdjustmentScore();
        isUpdate = false;
    }

}
