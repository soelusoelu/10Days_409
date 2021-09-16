using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float maxMoveSpeed = 45f;
    [SerializeField] private float speedLerpTime = 0.5f;
    [SerializeField] private float speedLerpMinusTime = 0.25f;
    private Timer speedLerpTimer;
    private Timer speedLerpMinusTimer;

    private void Start() {
        speedLerpTimer = new Timer();
        speedLerpTimer.SetLimitTime(speedLerpTime);

        speedLerpMinusTimer = new Timer();
        speedLerpMinusTimer.SetLimitTime(speedLerpMinusTime);
    }

    void Update() {
        float speed = moveSpeed;
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speedLerpMinusTimer.SetCurrentTime(speedLerpMinusTimer.GetLimitTime() * (1f - speedLerpTimer.Rate()));
            speedLerpTimer.Reset();
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (speedLerpTimer.IsTime()) {
                speed = maxMoveSpeed;
            } else {
                speedLerpTimer.Update();
                speed = Mathf.Lerp(moveSpeed, maxMoveSpeed, speedLerpTimer.Rate());
            }
        } else {
            if (speedLerpMinusTimer.IsTime()) {
                speed = moveSpeed;
            } else {
                speedLerpMinusTimer.Update();
                speed = Mathf.Lerp(maxMoveSpeed, moveSpeed, speedLerpMinusTimer.Rate());
            }
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.position += Vector3.right * h * speed * Time.deltaTime;
        transform.position += Vector3.up * v * speed * Time.deltaTime;
    }
}
