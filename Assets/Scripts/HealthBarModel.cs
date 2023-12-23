using UnityEngine;
using UnityEngine.UI;

public class HealthBarModel : MonoBehaviour {
    //MVP Model, bu sinif MonoBehaviour sinifindan miras almak zorunda degildir.
    [SerializeField] HealthBarModelSO _hbmSo;
    private Image _hpImg;
    private int _count;
    private void Start() {
        _hpImg = _hbmSo._healthPointSpr;
        _count = _hbmSo.quantity;
    }
    public Image GetHPImage() {
        return _hpImg;
    }

    public int GetHPCount() {
        return _count;
    }

    public int UpdateHPCount(int count) {
        _count -= count;
        return _count;
    }

}
