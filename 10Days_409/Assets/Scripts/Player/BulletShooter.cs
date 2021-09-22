using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reshotInterval = 0.25f;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _ShotAudioClip;
    private Timer timer;

    void Start() {
        timer = new Timer();
        timer.SetLimitTime(reshotInterval);
        timer.Overlimit();

        _AudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        timer.Update();

        if (!Input.GetKey(KeyCode.Space) && !Input.GetButton("Fire1")) {
            return;
        }
        if (!timer.IsTime()) {
            return;
        }

        timer.Reset();

        var newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;

        _AudioSource.PlayOneShot(_ShotAudioClip);
    }
}
