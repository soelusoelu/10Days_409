using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCollider : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private float explodeRange = 6f;
    [SerializeField] private int waitFrames = 1;
    private SphereCollider sphere = null;
    private int previousIndex = 0;

    private void Start() {
        sphere = GetComponent<SphereCollider>();
        sphere.radius = explodeRange / 2f;

        var p = Instantiate(particle);
        Destroy(p, 10f);
    }

    private void Update() {
        if (previousIndex == waitFrames) {
            Destroy(gameObject);
        } else {
            ++previousIndex;
        }
    }
}
