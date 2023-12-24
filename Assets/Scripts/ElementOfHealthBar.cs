using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementOfHealthBar : MonoBehaviour {

    //HealthBarView bu classin Presenteri gibi davranacaktir. Her class kendi gorevini yerine getirmektedir.
    public void AnimateHPAtTheEndOfLife() {
        StartCoroutine(IAnimateHPAtTheEndOfLife());
    }

    public IEnumerator IAnimateHPAtTheEndOfLife() {
        //HP death esnasinda gerceklesecek olan animasyon metodudur.
        float peakTime = 1f;
        float timeMeter = 0;
        Vector3 localScale = transform.localScale;

        while (timeMeter < peakTime) {
            timeMeter += Time.deltaTime;
            transform.localScale = localScale * EaseInOutElastic(1 - timeMeter);
            yield return null;
        }

        //timeMeter kusuratli kalabiliyor bu nedenle grafiksel bug yapiyor. Bu sayede grafik bug engellenir.
        timeMeter = peakTime;
        transform.localScale = localScale * EaseInOutElastic(1 - timeMeter);

        gameObject.SetActive(false);
    }

    public float EaseInOutElastic(float t, float coef = 3f) {
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
            return coef * (Mathf.Pow(3, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * fixedNumber)) / 2 + 1;
        }
    }
}
