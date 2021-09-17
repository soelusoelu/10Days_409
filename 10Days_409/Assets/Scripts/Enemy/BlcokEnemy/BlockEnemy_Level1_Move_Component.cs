using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy_Level1_Move_Component : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
        
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
