using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float[] nextCreateTimes;
    private List<GameObject> createdEnemys;
    private Timer timer;
    private int currentIndex = 0;
    private bool isLastCreatedWave = false;


    private void Start() {
        Debug.Assert(enemys.Length == nextCreateTimes.Length);

        createdEnemys = new List<GameObject>();
        timer = new Timer();

        if (enemys.Length > 0) {
            timer.SetLimitTime(nextCreateTimes[currentIndex]);
        } else {
            isLastCreatedWave = true;
        }
    }

    private void Update() {
        if (isLastCreatedWave) {
            for (int i = 0; i < createdEnemys.Count; i++) {
                if (createdEnemys[i] == null) {
                    createdEnemys.RemoveAt(i);
                }
            }
            return;
        }

        timer.Update();
        if (!timer.IsTime()) {
            return;
        }

        var newEnemy = Instantiate(enemys[currentIndex]);
        createdEnemys.Add(newEnemy);

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

    public bool IsDestroyedEnemys() {
        return (createdEnemys.Count == 0);
    }

    public bool CreatedLastEnemy() {
        return isLastCreatedWave;
    }
}
