using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeComponent : MonoBehaviour
{
    [SerializeField] private HitPoint _mHitPoint;
    [SerializeField] private Image _mHPGauge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float amount = (float) _mHitPoint.GetHp() / (float) _mHitPoint.GetMaxHp();

        _mHPGauge.fillAmount = amount;
    }
}
