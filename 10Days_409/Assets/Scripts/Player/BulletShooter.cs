using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Update() {
        if (!Input.GetKeyDown(KeyCode.Space)) {
            return;
        }

        var newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
    }
}
