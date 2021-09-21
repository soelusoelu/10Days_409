using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGhostComponent : MonoBehaviour
{

    [SerializeField] private float _mMoveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
