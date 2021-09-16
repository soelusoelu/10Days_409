using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    private EnemyCreater enemyCreater;
    private int currentIndex = 0;
    public delegate void CallbackOnEndWave();
    private CallbackOnEndWave onEndWave;
    private CallbackOnEndWave onAllEndWave;

    private void Start() {
        var newWave = waves[currentIndex];

        if (waves.Length > 0) {
            var newCreater = Instantiate(newWave);
            enemyCreater = newCreater.GetComponent<EnemyCreater>();
        }
    }

    private void Update() {
        if (!enemyCreater.CreatedLastEnemy()) {
            return;
        }

        if (enemyCreater.IsDestroyedEnemys()) {
            if (currentIndex == waves.Length) {
                return;
            }

            Debug.Log("enemys destroyed.");

            if (onEndWave != null) {
                onEndWave();
            }

            ChangeWave();
        }
    }

    public int GetCurrentWave() {
        return currentIndex;
    }

    public void OnEndWave(CallbackOnEndWave f) {
        onEndWave += f;
    }

    public void OnAllEndWave(CallbackOnEndWave f) {
        onAllEndWave += f;
    }

    private void ChangeWave() {
        ++currentIndex;
        if (currentIndex >= waves.Length) {
            Debug.Log("all ended waves.");
            if (onAllEndWave != null) {
                onAllEndWave();
            }
        } else {
            var newCreater = Instantiate(waves[currentIndex]);
            enemyCreater = newCreater.GetComponent<EnemyCreater>();
        }
    }
}
