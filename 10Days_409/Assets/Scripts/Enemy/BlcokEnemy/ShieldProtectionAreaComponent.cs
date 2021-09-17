using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtectionAreaComponent : MonoBehaviour
{
    [SerializeField] private GameObject _mShield;
    [SerializeField] private float _mDetectionTime = 1.0f;

    private Vector3 _mDetectionPoint;
    private Timer _mDetectionCoolTimer;

    private bool _mIsDetection;

    // Start is called before the first frame update
    void Start()
    {
        _mDetectionPoint = _mShield.transform.position;


        _mDetectionCoolTimer = new Timer();
        _mDetectionCoolTimer.SetLimitTime(_mDetectionTime);

        _mIsDetection = false;
    }

    // Update is called once per frame
    void Update()
    {
        _mDetectionCoolTimer.Update();
        MoveShield();
    }

    void MoveShield()
    {
        var currentPos = _mShield.gameObject.transform.position;
        var pos = Vector3.Lerp(currentPos, _mDetectionPoint, Time.deltaTime * 16.0f);

        _mShield.gameObject.transform.position = pos;
    }

    void OnTriggerStay(Collider coll)
    {
        if (!_mDetectionCoolTimer.IsTime()) return;

        if (coll.gameObject.tag == "Bullet")
        {
            _mDetectionPoint = coll.gameObject.transform.position;
            _mIsDetection = true;
        }
    }
}
