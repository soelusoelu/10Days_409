using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reshotInterval = 0.25f;
    private Timer timer;

    void Start() {
        timer = new Timer();
        timer.SetLimitTime(reshotInterval);
        timer.Overlimit();
    }

    void Update() {
        timer.Update();

        if (!Input.GetKey(KeyCode.Space)) {
            return;
        }
        if (!timer.IsTime()) {
            return;
        }

        timer.Reset();

        var newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
    }
}
