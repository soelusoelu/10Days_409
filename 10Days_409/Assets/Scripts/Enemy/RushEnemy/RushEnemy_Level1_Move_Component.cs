using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy_Level1_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.02f;
    [SerializeField] private float _mRotateTime = 3.0f;
    [SerializeField] private float _mEscapeTime = 3.0f;

    [SerializeField] private GameObject _mPlayer;

    private Timer _mEscapeTimer;
    [SerializeField] private bool _mIsEscape;


    // Start is called before the first frame update
    void Start()
    {
        _mPlayer = GameObject.Find("Player");

        _mEscapeTimer = new Timer();
        _mEscapeTimer.SetLimitTime(_mEscapeTime);

        _mIsEscape = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mPlayer == null) return;

        transform.LookAt(_mPlayer.transform);
        Move();

        if (!_mIsEscape) return;

        _mEscapeTimer.Update();
        if (_mEscapeTimer.IsTime())
        {
            _mIsEscape = false;
        }

        Escape();

    }

    void Escape()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed * 2.0f;

        transform.position = currentPos;
    }

    void Move()
    {
        var vec = transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed * 2.0f;

        transform.position = currentPos;
    }


}
