using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy_Level2_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.003f;
    [SerializeField] private float _mGenereateDefenceBlockSpeed = 1.0f;

    [SerializeField] private GameObject defenceBlockPrefab;

    private Timer _mGenerateDefenceBlockTimer;


    // Start is called before the first frame update
    private void Start()
    {
        _mGenerateDefenceBlockTimer = new Timer();
        _mGenerateDefenceBlockTimer.SetLimitTime(_mGenereateDefenceBlockSpeed);
    }

    public void SetDefenceBlockPrefab(GameObject prefab)
    {
        defenceBlockPrefab = prefab;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();


    }

    private void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
    }


}
