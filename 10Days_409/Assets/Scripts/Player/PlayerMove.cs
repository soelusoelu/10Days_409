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
        transform.Translate(
            h * speed * Time.deltaTime,
            v * speed * Time.deltaTime,
            0f
        );

        var cameraToPlayer = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);

        var topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, cameraToPlayer));
        var bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraToPlayer));

        var x = Mathf.Clamp(transform.position.x, topLeft.x + 1f, bottomRight.x - 1f);
        var y = Mathf.Clamp(transform.position.y, topLeft.y + 1f, bottomRight.y - 1f);

        transform.position = new Vector3(x, y, 0f);
    }
}
