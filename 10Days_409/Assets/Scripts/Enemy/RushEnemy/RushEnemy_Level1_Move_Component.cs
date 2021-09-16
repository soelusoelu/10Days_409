using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy_Level1_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.02f;
    [SerializeField] private float _mRotateTime = 3.0f;

    private Timer _mTimer;

    [SerializeField] private Vector3 _mMoveTargetAngle;

    // Start is called before the first frame update
    void Start()
    {
        _mMoveTargetAngle = transform.localEulerAngles;

        _mTimer = new Timer();
        _mTimer.SetLimitTime(_mRotateTime);
    }

    // Update is called once per frame
    void Update()
    {
        _mTimer.Update();

        if (_mTimer.IsTime())
        {
            _mTimer.Reset();
            ChangeRotate();
            return;
        }

        RotateUpdate();
        Move();
    }

    void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
    }

    void RotateUpdate()
    {
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, _mMoveTargetAngle, Time.time);
    }

    void ChangeRotate()
    {
        var currentRotation = transform.localEulerAngles;

        _mMoveTargetAngle = new Vector3(currentRotation.x, Random.Range(-120.0f, 120.0f),
            currentRotation.z);
    }
}
