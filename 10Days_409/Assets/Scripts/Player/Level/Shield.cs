using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private bool isRightRotation = true;
    [SerializeField] private float rotateTime = 5f;
    private Timer performanceTimer;
    private bool isStartPerforming = false;
    private Vector3 targetScale;
    private float targetRotationZ;

    private void Awake() {
        targetScale = transform.localScale;
        targetRotationZ = transform.localRotation.eulerAngles.z;
    }

    void Start() {
        performanceTimer = new Timer();
        performanceTimer.SetLimitTime(rotateTime);
    }

    void Update() {
        if (isStartPerforming) {
            UpdateStartPerformance();
        }
    }

    public void StartPerformance() {
        isStartPerforming = true;
        transform.localRotation = Quaternion.AngleAxis(targetRotationZ, Vector3.forward);
    }

    private void UpdateStartPerformance() {
        performanceTimer.Update();
        if (performanceTimer.IsTime()) {
            performanceTimer.Reset();
            isStartPerforming = false;
            return;
        }

        float x = Easing.EaseOutExpo(performanceTimer.Rate());
        x = Easing.EaseInCubic(x);
        float angle = x * 360f + targetRotationZ;
        if (isRightRotation) {
            angle *= -1f;
        }
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        x = Easing.EaseOutExpo(performanceTimer.Rate());
        float scaleX = Mathf.Lerp(0f, targetScale.x, x);
        float scaleY = Mathf.Lerp(0f, targetScale.y, x);
        float scaleZ = Mathf.Lerp(0f, targetScale.z, x);
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.tag.StartsWith("Enemy/")) {
            return;
        }

        gameObject.SetActive(false);
        Destroy(other.gameObject);
    }
}
