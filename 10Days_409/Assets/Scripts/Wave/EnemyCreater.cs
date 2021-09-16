using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float[] nextCreateTimes;
    private Timer timer;
    private int currentIndex = 0;
    private bool isLastCreatedWave = false;


    private void Start() {
        Debug.Assert(enemys.Length == nextCreateTimes.Length);

        timer = new Timer();

        if (enemys.Length > 0) {
            timer.SetLimitTime(nextCreateTimes[currentIndex]);
        } else {
            isLastCreatedWave = true;
        }
    }

    private void Update() {
        if (isLastCreatedWave) {
            return;
        }

        timer.Update();
        if (!timer.IsTime()) {
            return;
        }

        Instantiate(enemys[currentIndex]);

        ++currentIndex;
        if (currentIndex >= enemys.Length) {
            isLastCreatedWave = true;
            Debug.Log("last created.");
            return;
        }

        var t = nextCreateTimes[currentIndex];
        timer.SetLimitTime(t);
        timer.Reset();
    }

    public GameObject[] getEnemys() {
        return enemys;
    }

    public bool CreatedLastEnemy() {
        return isLastCreatedWave;
    }
}
