using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Level1_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 8.0f;

    private Vector3 _mBasePos;

    // Start is called before the first frame update
    void Start()
    {
        _mBasePos = transform.position;
    }

    public void SetObstacleCubePrefab(GameObject prefab)
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * _mMoveSpeed + _mBasePos.x, _mBasePos.y, _mBasePos.z);
    }
}
