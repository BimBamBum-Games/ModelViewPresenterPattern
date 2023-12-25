using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarModel : MonoBehaviourBase {
    //MVP Model, bu sinif MonoBehaviour sinifindan miras almak zorunda degildir.
    [SerializeField] HealthBarModelSO _hbmSo;
    private ElementOfHealthBar _elementOfHB;
    private int _count;

    private void Start() {
        ResetModel();
    }

    //Presenterda cagrilacaltir.
    public ElementOfHealthBar GetHPImage() {
        return _elementOfHB;
    }

    //Presenterda cagrilacaltir.
    public int GetHPCount() {
        return _count;
    }

    //Presenterda cagrilacaltir.
    public int UpdateHPCount(int count) {
        _count -= count;
        return _count;
    }

    public void ResetModel() {
        //Modeli hem initler hem de resetler.
        _elementOfHB = _hbmSo.elementOfHealthBar;
        _count = _hbmSo.quantity;
    }

}
