using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItemCreater : MonoBehaviour
{
    [SerializeField] private GameObject levelUpItem;
    [SerializeField] private Level playerLevel;
    [SerializeField] private float createInterval = 5f;
    [SerializeField] private float randomTime = 1f;
    [SerializeField] private float createPositionZ = 30f;
    private Timer createTimer;

    private void Start() {
        createTimer = new Timer();
        SetCreateTime();
    }

    private void Update() {
        if (playerLevel.GetLevel() >= playerLevel.GetMaxLevel()) {
            return;
        }

        createTimer.Update();
        if (createTimer.IsTime()) {
            createTimer.Reset();
            SetCreateTime();

            var newItem = Instantiate(levelUpItem);
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(0.0f, 1.0f);
            newItem.transform.position = new Vector3(x, y, createPositionZ);
        }
    }

    private void SetCreateTime() {
        float time =  Random.Range(createInterval - randomTime, createInterval + randomTime);
        createTimer.SetLimitTime(time);
    }
}
