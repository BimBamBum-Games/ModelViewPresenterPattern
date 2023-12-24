using UnityEngine;

public class HealthBarPresenter : MonoBehaviour {
    [SerializeField] HealthBarModel _healthBarModel;
    [SerializeField] HealthBarView _healthBarView;
    [SerializeField] int _dealtDamagePoint = 1;

    private void Start() {
        InitializeViewerUI();
        InitializeInputEvents();
    }

    private void InitializeViewerUI() {
        _healthBarView.HideGameState();
        _healthBarView.AddHealthPoint(_healthBarModel.GetHPImage(), _healthBarModel.GetHPCount());
    }

    public void TakeDamageOnClick() {
        //Modeli guncelleyerek kalan haklarin sayisini ogren. Eger haklar sifir olursa oyun bitti yazisi yazdir.
        int cnt = _healthBarModel.UpdateHPCount(_dealtDamagePoint);
        int cmp = Mathf.Max(0, cnt);

        if (cnt > -1) {
            _healthBarView.UpdateHealthBar(cnt);
        }

        if (cmp == 0) {
            Debug.LogWarning("Kalan Hak 0!");
            _healthBarView.ShowGameState();
        }
    }

    private void InitializeInputEvents() {
        //Daha az bagimlilik ile sagladi.
        _healthBarView.InitializeButtonEvents(TakeDamageOnClick);
    }
}
