using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    private EnemyCreater enemyCreater;
    private int currentIndex = 0;
    public delegate void CallbackOnEndWave();
    private CallbackOnEndWave onEndWave;
    private CallbackOnEndWave onAllEndWave;
    private bool isEnd = false;

    private void Start() {
        var newWave = waves[currentIndex];

        if (waves.Length > 0) {
            var newCreater = Instantiate(newWave);
            enemyCreater = newCreater.GetComponent<EnemyCreater>();
        }
    }

    private void Update() {
        if (isEnd) {
            return;
        }
        if (!enemyCreater.CreatedLastEnemy()) {
            return;
        }

        if (enemyCreater.IsDestroyedEnemys()) {
            Debug.Log("enemys destroyed.");

            onEndWave?.Invoke();

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
            Debug.Log("ended all waves.");
            onAllEndWave?.Invoke();
            isEnd = true;
            GameObject.Find("Score").GetComponent<Score>().DisableUpdate();
            --currentIndex;
        } else {
            var newCreater = Instantiate(waves[currentIndex]);
            enemyCreater = newCreater.GetComponent<EnemyCreater>();
        }
    }
}
