using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfHealthBar : MonoBehaviourBase {

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


    public void AnimateChild(Func<float, float> ease, float duration, bool reverse) {
        StartCoroutine(AnimTimer.IAnimateRectDefault(_childRct, EaseUtility.EaseInOutElastic, duration, reverse));
    }

    public void AnimateChildWithOnComplete(Func<float, float> ease, float duration, Action onComplete, bool reverse) {
        StartCoroutine(AnimTimer.IAnimateRectWithOnComplete(_childRct, ease, duration, onComplete, reverse));
    }

}
