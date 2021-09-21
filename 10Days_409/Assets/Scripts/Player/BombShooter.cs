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

    void Update() {
        timer.Update();

        if (!Input.GetKeyDown(KeyCode.Z) && !Input.GetButton("Bomb")) {
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
