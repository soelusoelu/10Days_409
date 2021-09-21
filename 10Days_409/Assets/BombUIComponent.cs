using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUIComponent : MonoBehaviour
{
    [SerializeField] private Image _mBombImage;
    [SerializeField] private BombShooter _mBombShooter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_mBombShooter == null) return;

        float ratio = _mBombShooter.GetShotRatio();

        _mBombImage.fillAmount = ratio;
    }
}
