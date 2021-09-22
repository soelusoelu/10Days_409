using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreUIComponent : MonoBehaviour
{
    [SerializeField] private Score _mScore;
    [SerializeField] private Text _mText;

    private int _mCurrentScore;


    // Start is called before the first frame update
    void Start()
    {
        _mCurrentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float score = Mathf.Clamp(Mathf.Lerp(_mCurrentScore, (float)_mScore.GetScore() * 1.2f, Time.deltaTime), 0, _mScore.GetScore());

        _mText.text = String.Format("{0:000000}", score);

    }
}
