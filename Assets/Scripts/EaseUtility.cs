using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EaseUtility {
    //Custom Ease Metodlari icin Tanimli Helper Classtir.
    public static float EaseInOutElastic(float t) {
        //Animation saglamak maksadiyla kullanilacak olan ease metodudur.
        float fixedNumber = (2 * Mathf.PI) / 4.5f;

        if (t == 0) {
            return 0;
        }
        else if (t == 1) {
            return 1;
        }
        else if (t < 0.5) {
            return -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * fixedNumber)) / 2;
        }
        else {
            return (Mathf.Pow(3, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * fixedNumber)) / 2 + 1;
        }
    }

    public static float EaseInElastic(float t) {
        //Animation saglamak maksadiyla kullanilacak olan ease metodudur.
        float c4 = (2 * Mathf.PI) / 3;

        if (t == 0) {
            return 0;
        }
        else if (t == 1) {
            return 1;
        }
        else {
            return -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * c4);
        }
    }
}
