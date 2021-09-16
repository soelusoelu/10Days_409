using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCollider : MonoBehaviour
{
    [SerializeField] private float explodeRange = 6f;
    private SphereCollider sphere = null;
    private bool waitOneFrame = false;

    private void Start() {
        sphere = GetComponent<SphereCollider>();
        sphere.radius = explodeRange / 2f;
    }

    private void Update() {
        if (waitOneFrame) {
            Destroy(gameObject);
        }

        waitOneFrame = true;
    }
}
