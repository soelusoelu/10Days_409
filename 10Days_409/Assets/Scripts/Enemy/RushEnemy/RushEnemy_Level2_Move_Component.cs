using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy_Level2_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 1.0f;
    [SerializeField] private float _mMoveTime = 0.1f;
    [SerializeField] private float _mCurrentMoveTime;

    private Vector3 _mMoveTargetPos;
    [SerializeField] private int _mMoveTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        _mMoveTargetPos = transform.localPosition;
        _mMoveTurn = 0;

    }

    // Update is called once per frame
    void Update()
    {

        UpdateTimer();
        if (IsMove())
        {
            MoveTarget();
            ResetTime();

            _mMoveTurn++;
            _mMoveTurn = Mathf.Clamp(_mMoveTurn % 3, 0, 3);
            return;
        }

        UpdateMove();
    }

    void MoveTarget()
    {
        var currentPos = transform.position;

        _mMoveTargetPos = currentPos + (Vector3.back * _mMoveSpeed);
    }

    void UpdateMove()
    {
        var currentPos = transform.position;

        if (_mMoveTurn == 2)
        {
            var pos = Vector3.Lerp(currentPos, _mMoveTargetPos, Time.deltaTime * 8.0f);
            transform.position = pos;
        }
        else
        {
            var pos = Vector3.Lerp(currentPos, _mMoveTargetPos, Time.deltaTime * 4.0f);
            transform.position = pos;
        }

    }

    bool IsMove()
    {
        return _mCurrentMoveTime >= _mMoveTime;
    }

    void UpdateTimer()
    {
        _mCurrentMoveTime += Time.deltaTime;
    }

    void ResetTime()
    {
        _mCurrentMoveTime = 0.0f;
    }
}
