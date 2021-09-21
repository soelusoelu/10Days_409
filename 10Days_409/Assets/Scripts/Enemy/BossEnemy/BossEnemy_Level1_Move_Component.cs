using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Level1_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 8.0f;
    [SerializeField] private float _mBulletGenerateRate = 0.1f;
    [SerializeField] private float _mGhostGenerateRate = 3.0f;

    private Timer _mBulletGenerateRateTimer;
    private Timer _mGhostGenerateRateTimer;

    private GameObject _mGhostPrefab;
    private GameObject _mBulletPrefab;

    private Vector3 _mBasePos;

    // Start is called before the first frame update
    void Start()
    {
        _mBasePos = transform.position;

        _mBulletGenerateRateTimer = new Timer();
        _mBulletGenerateRateTimer.SetLimitTime(_mBulletGenerateRate);

        _mGhostGenerateRateTimer = new Timer();
        _mGhostGenerateRateTimer.SetLimitTime(_mGhostGenerateRate);
    }

    public void SetGhostPrefab(GameObject prefab)
    {
        _mGhostPrefab = prefab;
    }

    public void SetBulletPrefab(GameObject prefab)
    {
        _mBulletPrefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        GenerateBullet();
        GenerateGhost();
    }


    private void Move()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * _mMoveSpeed + _mBasePos.x, _mBasePos.y, _mBasePos.z);
    }

    void GenerateGhost()
    {
        _mGhostGenerateRateTimer.Update();

        if (!_mGhostGenerateRateTimer.IsTime()) return;

        _mGhostGenerateRateTimer.Reset();

        var prefab = GameObject.Instantiate(_mGhostPrefab);
        Destroy(prefab, 12.0f);

        var setPosition = transform.position + -transform.forward * 2.0f;

        prefab.transform.position = setPosition;

    }

    void GenerateBullet()
    {
        _mBulletGenerateRateTimer.Update();

        if (!_mBulletGenerateRateTimer.IsTime()) return;
        _mBulletGenerateRateTimer.Reset();


        var prefab = GameObject.Instantiate(_mBulletPrefab);
        Destroy(prefab, 12.0f);

        var setPosition = transform.position + -transform.forward * 2.0f;

        prefab.transform.position = setPosition;

    }
}
