using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Level2_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 8.0f;
    [SerializeField] private float _mAttackTime = 6.0f;
    [SerializeField] private float _mAttackCoolTime = 6.0f;
    [SerializeField] private float _mBulletGenerateRate = 0.06f;

    private Vector3 _mBasePos;
    private GameObject enemyBulletPrefab;

    private Timer _mBulletGenerateRateTimer;
    private Timer _mAttackCoolTimer;
    private Timer _mAttackTimer;

    private bool _mIsAttack;

    void Start()
    {
        _mBasePos = transform.position;

        _mIsAttack = false;

        _mAttackTimer = new Timer();
        _mAttackCoolTimer = new Timer();
        _mBulletGenerateRateTimer = new Timer();

        _mAttackCoolTimer.SetLimitTime(_mAttackCoolTime);
        _mAttackTimer.SetLimitTime(_mAttackTime);
        _mBulletGenerateRateTimer.SetLimitTime(_mBulletGenerateRate);
    }

    public void SetBulletPrefab(GameObject prefab)
    {
        enemyBulletPrefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mIsAttack)
        {
            _mAttackTimer.Update();

            if (_mAttackTimer.IsTime())
            {
                ResetAttack();
                return;
            }

            Attack();
            return;
        }

        _mAttackCoolTimer.Update();

        if (_mAttackCoolTimer.IsTime())
        {
            _mAttackCoolTimer.Reset();
            _mIsAttack = true;
        }

        Move();
    }

    void ResetAttack()
    {
        _mAttackTimer.Reset();
        _mIsAttack = false;
    }

    void Attack()
    {
        _mBulletGenerateRateTimer.Update();

        if (!_mBulletGenerateRateTimer.IsTime()) return;
        _mBulletGenerateRateTimer.Reset();


        var prefab0 = GameObject.Instantiate(enemyBulletPrefab);
        Destroy(prefab0, 12.0f);

        var prefab1 = GameObject.Instantiate(enemyBulletPrefab);
        Destroy(prefab1, 12.0f);

        var setPosition0 = transform.position + -transform.forward * 2.0f + new Vector3(-2, 0, 0);
        var setPosition1 = transform.position + -transform.forward * 2.0f + new Vector3(2, 0, 0);

        prefab0.transform.position = setPosition0;
        prefab1.transform.position = setPosition1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explode")
        {
            ResetAttack();
        }

    }

    private void Move()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * _mMoveSpeed + _mBasePos.x, _mBasePos.y, _mBasePos.z);
    }

}
