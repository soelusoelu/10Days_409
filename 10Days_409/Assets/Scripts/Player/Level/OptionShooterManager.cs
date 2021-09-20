using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionShooterManager : MonoBehaviour
{
    private OptionShooter[] options;

    private void Start() {
        int childCount = transform.childCount;
        options = new OptionShooter[childCount];
        for (int i = 0; i < transform.childCount; i++) {
            var c = transform.GetChild(i);
            options[i] = c.GetComponent<OptionShooter>();
        }
    }

    public void StartPerformance() {
        foreach (var o in options) {
            o.gameObject.SetActive(true);
            o.StartPerformance();
        }
    }

    public void EndPerformance() {
        foreach (var o in options) {
            o.EndPerformance();
        }
    }
}
