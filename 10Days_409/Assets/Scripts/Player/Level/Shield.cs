using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Timer performanceTimer;
    private bool isStartPerforming = false;
    private Vector3 targetPos;
    private Vector3 performanceStartPos;

    private void Awake() {
        targetPos = transform.localPosition;
    }

    void Start() {
        performanceTimer = new Timer();
        performanceTimer.SetLimitTime(1f);
    }

    void Update() {
        if (isStartPerforming) {
            UpdateStartPerformance();
        }
    }

    public void StartPerformance() {
        isStartPerforming = true;

        var pos = targetPos + Vector3.back * 5f;
        transform.localPosition = pos;
        performanceStartPos = pos;
    }

    private void UpdateStartPerformance() {
        performanceTimer.Update();
        if (performanceTimer.IsTime()) {
            performanceTimer.Reset();
            isStartPerforming = false;
            return;
        }

        transform.localPosition = Vector3.Lerp(performanceStartPos, targetPos, performanceTimer.Rate());
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.tag.StartsWith("Enemy/")) {
            return;
        }

        gameObject.SetActive(false);
        Destroy(other.gameObject);
    }
}
