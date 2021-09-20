using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreuIComponent : MonoBehaviour
{
    [SerializeField] private Score _mScore;
    [SerializeField] private Text _mText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mText.text = String.Format("{0:000000}", _mScore.GetScore());
    }
}
