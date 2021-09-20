using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Level3_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 12.0f;
    [SerializeField] private float _mObstacleGenerateTime = 3.0f;
    [SerializeField] private float _mObstacleGenerateCoolTime = 3.0f;
    [SerializeField] private float _mObstacleGenerateRate = 0.1f;

   private GameObject _mEnemyObstacleCubePrefab;

    private Vector3 _mBasePos;

    private Timer _mObstacleGenerateTimer;
    private Timer _mObstacleGenerateCoolTimer;
    private Timer _mObstacleGenerateRateTimer;

    private bool _mIsObstacleGenerate;


    // Start is called before the first frame update
    void Start()
    {
        _mBasePos = transform.position;

        _mObstacleGenerateCoolTimer = new Timer();
        _mObstacleGenerateCoolTimer.SetLimitTime(_mObstacleGenerateCoolTime);

        _mObstacleGenerateRateTimer = new Timer();
        _mObstacleGenerateRateTimer.SetLimitTime(_mObstacleGenerateRate);

        _mObstacleGenerateTimer = new Timer();
        _mObstacleGenerateTimer.SetLimitTime(_mObstacleGenerateTime);

        _mIsObstacleGenerate = false;
    }

    public void SetObstacleCubePrefab(GameObject prefab)
    {
        _mEnemyObstacleCubePrefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (_mIsObstacleGenerate)
        {
            _mObstacleGenerateTimer.Update();
            GenerateObstacleCube();

            if (_mObstacleGenerateTimer.IsTime())
            {
                _mObstacleGenerateTimer.Reset();
                _mIsObstacleGenerate = false;
            }

            return;
        }

        _mObstacleGenerateCoolTimer.Update();

        if (_mObstacleGenerateCoolTimer.IsTime())
        {
            _mObstacleGenerateCoolTimer.Reset();
            _mIsObstacleGenerate = true;
        }

    }


    private void Move()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * _mMoveSpeed + _mBasePos.x, _mBasePos.y, _mBasePos.z);
    }

    void GenerateObstacleCube()
    {
        _mObstacleGenerateRateTimer.Update();

        if (!_mObstacleGenerateRateTimer.IsTime()) return;
        _mObstacleGenerateRateTimer.Reset();
        

        var prefab = GameObject.Instantiate(_mEnemyObstacleCubePrefab);
        Destroy(prefab, 12.0f);

        var setPosition = transform.position + -transform.forward * 2.0f;

        prefab.transform.position = setPosition;

    }
}
