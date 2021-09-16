using UnityEngine;
using System.Collections.Generic;

public class BlockEnemy_Level3_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.01f;
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

        _mGenerateDefenceBlockTimer.Update();

        if (_mGenerateDefenceBlockTimer.IsTime())
        {
            GenerateDefenceBlock();
            _mGenerateDefenceBlockTimer.Reset();
            return;
        }
    }

    private void GenerateRotateBlock()
    {

    }

    private void GenerateDefenceBlock()
    {

        var prefab = GameObject.Instantiate(defenceBlockPrefab);

        var setPosition = transform.position + -transform.forward * 10.0f;

        Debug.Log("ê∂ê¨à íu:" + setPosition);

        prefab.transform.position = setPosition;
    }

    private void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
    }



}
