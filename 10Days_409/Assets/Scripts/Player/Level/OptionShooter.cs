using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reshotInterval = 0.5f;
    [SerializeField] private float rotateTime = 2f;
    [SerializeField] private bool isRightRotation = true;
    private Timer timer;
    private Timer rotateTimer;
    private Timer performanceTimer;
    private bool isStartPerforming = false;
    private bool isEndPerforming = false;
    private Vector3 optionPos;
    private Vector3 performancePos;

    private void Awake() {
        optionPos = transform.localPosition;
    }

    void Start() {
        timer = new Timer();
        timer.SetLimitTime(reshotInterval);
        timer.Overlimit();

        rotateTimer = new Timer();
        rotateTimer.SetLimitTime(rotateTime);

        performanceTimer = new Timer();
        performanceTimer.SetLimitTime(1f);
    }

    void Update() {
        Rotate();

        if (isStartPerforming) {
            UpdateStartPerformance();
            return;
        } else if (isEndPerforming) {
            UpdateEndPerformance();
            return;
        }

        CreateBullet();
    }

    private void CreateBullet() {
        timer.Update();

        if (!timer.IsTime()) {
            return;
        }

        timer.Reset();

        var newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
    }

    private void Rotate() {
        rotateTimer.Update();
        if (rotateTimer.IsTime()) {
            rotateTimer.Reset();
        }

        float x = Easing.EaseInOutCubic(rotateTimer.Rate());
        if (isRightRotation) {
            x *= -1f;
        }
        transform.rotation = Quaternion.AngleAxis(x * 360f, Vector3.forward);
    }

    public void StartPerformance() {
        isStartPerforming = true;

        var pos = optionPos + Vector3.back * 5f;
        transform.localPosition = pos;
        performancePos = pos;
    }

    private void UpdateStartPerformance() {
        performanceTimer.Update();
        if (performanceTimer.IsTime()) {
            performanceTimer.Reset();
            isStartPerforming = false;
            return;
        }

        transform.localPosition = Vector3.Lerp(performancePos, optionPos, performanceTimer.Rate());
    }

    public void EndPerformance() {
        isEndPerforming = true;

        performancePos = optionPos + Vector3.back * 5f;
    }

    private void UpdateEndPerformance() {
        performanceTimer.Update();
        if (performanceTimer.IsTime()) {
            performanceTimer.Reset();
            isEndPerforming = false;
            gameObject.SetActive(false);
            return;
        }

        transform.localPosition = Vector3.Lerp(optionPos, performancePos, performanceTimer.Rate());
    }
}
