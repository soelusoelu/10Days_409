using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private Shield[] shields;

    private void Start() {
        int childCount = transform.childCount;
        shields = new Shield[childCount];
        for (int i = 0; i < childCount; i++) {
            shields[i] = transform.GetChild(i).GetComponent<Shield>();
        }
    }

    public void StartPerformance() {
        foreach (var s in shields) {
            s.gameObject.SetActive(true);
            s.StartPerformance();
        }
    }
}
