using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private List<Shield> shields;

    private void Start() {
        shields = new List<Shield>();
        var child = transform.GetChild(0);
        shields.Add(child.GetComponent<Shield>());
        var child2 = child.GetChild(0);
        shields.Add(child2.GetComponent<Shield>());
        var child3 = child2.GetChild(0);
        shields.Add(child3.GetComponent<Shield>());
    }

    public void StartPerformance() {
        foreach (var s in shields) {
            s.gameObject.SetActive(true);
            s.StartPerformance();
        }
    }
}
