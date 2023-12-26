using TMPro;
using UnityEngine;

public class ElementTmpOfView : MonoBehaviourBase {
    //TextMeshProGUI rect tipindeki referanslara eklenecek olan komponenttir. Animasyon saglamak amaciyladir.

    RectTransform _rectTransform;
    Vector3 _position, _localScale;
    Quaternion _rotation;

    //Disaridan text atamasi saglamak amaciyla propertydir.
    public TextMeshProUGUI TmpGui { get; private set; }

    void Start () {
        _rectTransform = GetComponent<RectTransform>();
        BackUpInitRectValues();

        TmpGui = GetComponent<TextMeshProUGUI>();

        if( _rectTransform == null ) {
            Dlog("RectTransform Bulunamadi!");
        }

        if(TmpGui == null ) {
            Dlog("Text Mesh Pro GUI Komponenti Bulunamadi!");
        }
    }

    public void BackUpInitRectValues() {
        //RectTransformInit Degerleri Yedekle.
        _position = _rectTransform.position;
        _localScale = _rectTransform.localScale;
        _rotation = _rectTransform.rotation;
    }

    public void RecoveryInitRectValues() {
        //RectTransform Init Degerleri Geri Yukle.
        _rectTransform.position = _position;
        _rectTransform.localScale = _localScale;
        _rectTransform.rotation = _rotation;
    }

    public void AnimateItself() {
        StopAllCoroutines();
        RecoveryInitRectValues();
        Dlog("TMpro coroutine metoduna giris yapildi.");
        StartCoroutine(AnimTimer.IAnimateRectDefault(_rectTransform, EaseUtility.EaseInOutElastic, 0.6f, false));
    }
}
