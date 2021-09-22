using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing
{
    static public float EaseInCubic(float x) {
        return (x * x * x);
    }

    static public float EaseOutCubic(float x) {
        return 1 - Mathf.Pow(1f - x, 3f);
    }

    static public float EaseInQuad(float x) {
        return (x * x);
    }

    static public float EaseInOutCubic(float x) {
        return x < 0.5f ? 4f * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }

    static public float EaseInOutQuad(float x) {
        return x < 0.5f ? 2f * x * x : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
    }

    static public float EaseOutExpo(float x) {
        return (x == 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * x);
    }
}
