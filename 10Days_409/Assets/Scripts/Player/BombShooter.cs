using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private float reshotInterval = 20f;
    private Timer timer;

    void Start() {
        timer = new Timer();
        timer.SetLimitTime(reshotInterval);
        timer.Overlimit();
    }

    public float GetShotRatio()
    {
        return timer.Rate();
    }

    void Update() {
        timer.Update();

        if (!Input.GetKeyDown(KeyCode.Z)) {
            return;
        }
        if (!timer.IsTime()) {
            return;
        }

        timer.Reset();

        var newBullet = Instantiate(bomb);
        newBullet.transform.position = transform.position;
    }
}
