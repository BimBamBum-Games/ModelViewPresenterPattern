using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAlphaHittable : MonoBehaviourBase {
    //Image Transparency durumuna gore buttonlara tiklanabilirligin ayarlanmasi icin olusturulmus siniftir.
    private Image _buttonImg;
    void Start () {
        _buttonImg = GetComponent<Image>();
        if(_buttonImg == null) {
            Dlog("Button veya Buttona Ait Image Komponenti Bulunamadi!");
        }

        //Transparent olan kisimlarda 127 altina duserse tiklanamaz olarak ayarlanir.
        _buttonImg.alphaHitTestMinimumThreshold = 0.1f;
    }
}
