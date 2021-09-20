using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItemCreater : MonoBehaviour
{
    [SerializeField] private GameObject levelUpItem;
    [SerializeField] private Vector3[] itemPosition;
    [SerializeField] private float[] nextCreateTimes;
    [SerializeField] private float offsetItemPositionZ = 5f;
    private Timer timer;
    private int currentIndex = 0;
    private bool isLastCreated = false;

    private void Start() {
        Debug.Assert(itemPosition.Length == nextCreateTimes.Length);

        timer = new Timer();

        if (itemPosition.Length > 0) {
            timer.SetLimitTime(nextCreateTimes[currentIndex]);
        } else {
            isLastCreated = true;
        }
    }

    private void Update() {
        if (isLastCreated) {
            return;
        }

        timer.Update();
        if (!timer.IsTime()) {
            return;
        }

        var newItem = Instantiate(levelUpItem);
        newItem.transform.position = itemPosition[currentIndex] + Vector3.forward * offsetItemPositionZ;

        ++currentIndex;
        if (currentIndex < itemPosition.Length) {
            var t = nextCreateTimes[currentIndex];
            timer.SetLimitTime(t);
            timer.Reset();
        } else {
            isLastCreated = true;
        }
    }
}
