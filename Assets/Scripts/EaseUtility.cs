using System;
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

public static class AnimTimer {
    public static IEnumerator IAnimateRect(RectTransform rec, Func<float, float> ease, float duration, Action onStart = null, Action onUpdate = null, Action onComplete = null, bool reverse = false) {
        float peakTime = 1f;
        float timeMeter = reverse ? 0 : peakTime;
        float inverseDivisionFraction = 1 / duration;
        Vector3 localScale = rec.localScale;
        onStart?.Invoke();
        while (reverse ? (timeMeter < peakTime) : (timeMeter > 0)) {
            timeMeter += (reverse ? 1 : -1) * Time.deltaTime * inverseDivisionFraction;

            if (ease != null) rec.localScale = localScale * ease(timeMeter);
            else rec.localScale = localScale * timeMeter;

            onUpdate?.Invoke();
            yield return null;
        }

        // timeMeter kusuratli kalabiliyor bu nedenle grafiksel bug yapiyor. Bu sayede grafik bug engellenir.
        timeMeter = reverse ? peakTime : 0;
        rec.localScale = localScale * timeMeter;

        onComplete?.Invoke();
    }

    public static IEnumerator IAnimateRectDefault(RectTransform rt,
        Func<float, float> ease,
        float duration,
        bool reverse = false) {
        yield return IAnimateRect(rt, ease, duration, null, null, null, reverse);
    }

    public static IEnumerator IAnimateRectWithOnComplete(RectTransform rt,
        Func<float, float> ease,
        float duration,
        Action onCompleted,
        bool reverse = false) {
        yield return IAnimateRect(rt, ease, duration, null, null, onCompleted, reverse);
    }
}
