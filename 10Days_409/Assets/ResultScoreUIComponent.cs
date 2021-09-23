using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreUIComponent : MonoBehaviour
{
    [SerializeField] private Score _mScore;
    [SerializeField] private Text _mText;

    private float _mCurrentScore;
    private Timer _mTimer;


    // Start is called before the first frame update
    void Start()
    {
        _mCurrentScore = 0;

        _mTimer = new Timer();
        _mTimer.SetLimitTime(10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _mTimer.Update();
        _mText.text = String.Format("{0:000000}", _mCurrentScore);


        if (_mTimer.IsTime())
        {
            _mCurrentScore = _mScore.GetScore();
            return;
        }

        _mCurrentScore = Mathf.Clamp(Mathf.Lerp(_mCurrentScore, (float)_mScore.GetScore() * 1.0f, _mTimer.Rate()), 0, _mScore.GetScore());

    }
}
