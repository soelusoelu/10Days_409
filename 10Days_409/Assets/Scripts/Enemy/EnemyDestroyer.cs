using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public delegate void CallbackOnDestroyEnemy(GameObject enemy, bool outOfArea);
    static private CallbackOnDestroyEnemy onDestroyEnemy;

    static public void DestroyEnemy(GameObject enemy, bool outOfArea = false) {
        onDestroyEnemy?.Invoke(enemy, outOfArea);
        Destroy(enemy);
    }

    static public void OnDestroyEnemy(CallbackOnDestroyEnemy f) {
        onDestroyEnemy += f;
    }
}
