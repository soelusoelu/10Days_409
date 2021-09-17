using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBlock_MoveComponent : MonoBehaviour
{
    private bool _mIsMove;

    [SerializeField] private float _mMoveSpeed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        _mIsMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_mIsMove) return;

        Move();
    }

    void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            GameObject.Destroy(collider.gameObject);
            _mIsMove = true;
        }

        if (collider.gameObject.tag == "Player")
        {
            // TODO: Ç†Ç∆Ç≈É_ÉÅÅ[ÉWèàóù
        }
    }
}
