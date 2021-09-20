using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Vector3[] enemysPosition;
    [SerializeField] private float[] nextCreateTimes;
    [SerializeField] private float offsetEnemyPositionZ = 5f;
    private List<GameObject> createdEnemys;
    private Timer timer;
    private int currentIndex = 0;
    private bool isLastCreatedWave = false;
    public delegate void CallbackOnDeadEnemy();
    private CallbackOnDeadEnemy onDeadEnemy;

    private void Start() {
        Debug.Assert(enemys.Length == nextCreateTimes.Length);
        Debug.Assert(enemys.Length == enemysPosition.Length);

        createdEnemys = new List<GameObject>();
        timer = new Timer();

        if (enemys.Length > 0) {
            timer.SetLimitTime(nextCreateTimes[currentIndex]);
        } else {
            isLastCreatedWave = true;
        }
    }

    private void Update() {
        for (int i = 0; i < createdEnemys.Count; i++) {
            if (createdEnemys[i] == null) {
                onDeadEnemy?.Invoke();
                createdEnemys.RemoveAt(i);
            }
        }

        if (isLastCreatedWave) {
            return;
        }

        timer.Update();
        if (!timer.IsTime()) {
            return;
        }

        var newEnemy = Instantiate(enemys[currentIndex]);
        newEnemy.transform.position = enemysPosition[currentIndex] + Vector3.forward * offsetEnemyPositionZ;
        createdEnemys.Add(newEnemy);

        ++currentIndex;
        if (currentIndex >= enemys.Length) {
            isLastCreatedWave = true;
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

    public void OnDeadEnemy(CallbackOnDeadEnemy f) {
        onDeadEnemy += f;
    }
}
