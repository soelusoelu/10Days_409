using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    private EnemyCreater enemyCreater;
    private int currentIndex = 0;

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

        var enemys = enemyCreater.getEnemys();
        Debug.Log("enemys count: " + enemys.Length.ToString());
        for (int i = 0; i < enemys.Length; i++) {
            if (enemys[i] != null) {
                return;
            }
        }

        Debug.Log("enemys destroyed.");
    }
}
