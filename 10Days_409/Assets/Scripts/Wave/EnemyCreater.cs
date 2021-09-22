using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Vector3[] enemysPosition;
    [SerializeField] private float[] nextCreateTimes;
    [SerializeField] private float offsetEnemyPositionZ = 5f;
    private Score score;
    public delegate void CallbackOnDestroyEnemy();
    private List<GameObject> createdEnemys;
    private List<string> createdEnemysTag;
    private List<float> createdEnemysPosZ;
    private Timer timer;
    private int currentIndex = 0;
    private bool isLastCreatedWave = false;

    private void Start() {
        Debug.Assert(enemys.Length == nextCreateTimes.Length);
        Debug.Assert(enemys.Length == enemysPosition.Length);

        createdEnemys = new List<GameObject>();
        createdEnemysTag = new List<string>();
        createdEnemysPosZ = new List<float>();
        timer = new Timer();

        if (enemys.Length > 0) {
            timer.SetLimitTime(nextCreateTimes[currentIndex]);
        } else {
            isLastCreatedWave = true;
        }

        score = GameObject.Find("Score").GetComponent<Score>();
    }

    private void Update() {
        bool isUpdatePosZ = true;
        for (int i = 0; i < createdEnemys.Count; i++) {
            if (createdEnemys[i] == null) {
                score.AddScore(createdEnemysTag[i], createdEnemysPosZ[i]);
                createdEnemys.RemoveAt(i);
                createdEnemysTag.RemoveAt(i);
                createdEnemysPosZ.RemoveAt(i);

                isUpdatePosZ = false;

                Debug.Assert(createdEnemys.Count == createdEnemysTag.Count);
                Debug.Assert(createdEnemys.Count == createdEnemysPosZ.Count);
            }
        }
        if (isUpdatePosZ) {
            for (int i = 0; i < createdEnemys.Count; i++) {
                createdEnemysPosZ[i] = createdEnemys[i].transform.position.z;
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
        createdEnemysTag.Add(newEnemy.tag);
        createdEnemysPosZ.Add(newEnemy.transform.position.z);

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
}
