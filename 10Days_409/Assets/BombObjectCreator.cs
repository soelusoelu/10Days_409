using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObjectCreator : MonoBehaviour
{
    [SerializeField] private GameObject _BombObjectPrefab;
    [SerializeField] private Vector3 _GenerateRangeMin;
    [SerializeField] private Vector3 _GenerateRangeMax;
    [SerializeField] private Vector3 _GenerateBasePosition;

    [SerializeField] private float _GenerateInterval;


    private Timer createTimer;

    // Start is called before the first frame update
    void Start()
    {
        createTimer = new Timer();
        createTimer.SetLimitTime(_GenerateInterval + Random.Range(-_GenerateInterval * 0.5f, _GenerateInterval * 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGenerate())

        {
            var instance = Instantiate(_BombObjectPrefab);

            float x = Random.Range(_GenerateRangeMin.x, _GenerateRangeMax.x);
            float y = Random.Range(_GenerateRangeMin.y, _GenerateRangeMax.y);
            float z = Random.Range(_GenerateRangeMin.z, _GenerateRangeMax.z);

            instance.transform.position = _GenerateBasePosition + new Vector3(x, y, z);
            createTimer.Reset();
            createTimer.SetLimitTime(_GenerateInterval + Random.Range(-_GenerateInterval * 0.5f, _GenerateInterval * 0.5f));
        }
    }

    bool IsGenerate()
    {
        createTimer.Update();
        return createTimer.IsTime();
    }
}
