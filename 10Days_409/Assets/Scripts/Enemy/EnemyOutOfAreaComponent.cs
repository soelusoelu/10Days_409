using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOutOfAreaComponent : MonoBehaviour
{
    [SerializeField] private float _mOutArea = -2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOutofArea())
        {
            Destroy(gameObject);
        }
    }

    bool IsOutofArea()
    {
        return transform.position.z <= _mOutArea;
    }
}
