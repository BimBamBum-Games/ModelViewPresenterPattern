using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfHealthBar : MonoBehaviour {

    //HealthBarView bu classin Presenteri gibi davranacaktir. Her class kendi gorevini yerine getirmektedir.
    [SerializeField] RectTransform _childRct;
    private Vector3 _childLocalScale, _childLocalPosition;
    private Quaternion _childLocalRotation;
    private Image _childImg;
    private void Start() {
        _childImg = _childRct.GetComponent<Image>();
        CacheInitValues();
    }

    public RectTransform GetChildRect() {
        //Image komponentini tasiyan animasyon child rect. Bu sekilde Layout update sirasinda olusacak problemlerden kacinilmis olunur.
        return _childRct;
    }

    public Image GetImageComponent() {
        //Bu HorizontalLayout tipi komponentlerin animasyonlari esnasinda refresh problemi yaratabildiginden dolayi Image komponentleri disable enable seklinde calistirilacak.
        return _childImg;
    }

    public void CacheInitValues() {
        //Ilk degerleri veya o anlik degerler yedeklenir.
        _childLocalPosition = _childRct.localPosition;
        _childLocalScale = _childRct.localScale;
        _childLocalRotation = _childRct.localRotation;
    }

    public void SetOrResetTransformValues() {
        //Yedeklenen ilk degerleri veya herhangi bir anda yedeklenmis olan degerleri setler. Presenter cagirir.
        _childRct.localPosition = _childLocalPosition;
        _childRct.localScale = _childLocalScale;
        _childRct.localRotation = _childLocalRotation;
    }

    public void AnimateWithDefault(Func<float, float> ease, float duration, bool reverse) {
        StartCoroutine(IAnimateHPAtTheEndOfLife(ease, duration, null, null, null, reverse));
    }

    public void AnimateWithOnComplete(Func<float, float> ease, float duration, Action onComplete, bool reverse) {
        StartCoroutine(IAnimateHPAtTheEndOfLife(ease, duration, null, null, onComplete, reverse));
    }

    public void AnimateHPAtTheEndOfLife(Func<float, float> ease, float duration, Action onStart, Action onUpdate, Action onComplete, bool reverse) {
        //Parametreler HealthBar tarafindan saglanacaktir.
        StartCoroutine(IAnimateHPAtTheEndOfLife(ease, duration, onStart, onUpdate, onComplete, reverse));
    }

    public IEnumerator IAnimateHPAtTheEndOfLife(Func<float, float> ease, float duration, Action onStart = null, Action onUpdate = null, Action onComplete = null, bool reverse = false) {
        float peakTime = 1f;
        float timeMeter = reverse ? 0 : peakTime;
        float inverseDivisionFraction = 1 / duration;
        Vector3 localScale = _childRct.localScale;
        onStart?.Invoke();
        while (reverse ? (timeMeter < peakTime) : (timeMeter > 0)) {
            timeMeter += (reverse ? 1 : -1) * Time.deltaTime * inverseDivisionFraction;
            _childRct.localScale = localScale * ease(timeMeter);
            onUpdate?.Invoke();
            yield return null;
        }

        // timeMeter kusuratli kalabiliyor bu nedenle grafiksel bug yapiyor. Bu sayede grafik bug engellenir.
        timeMeter = reverse ? peakTime : 0;
        _childRct.localScale = localScale * ease(timeMeter);

        onComplete?.Invoke();
    }


}
