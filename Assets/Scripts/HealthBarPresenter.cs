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
        int cnt = _healthBarModel.UpdateHPCount(_dealtDamagePoint);
        if (cnt < 0) {
            Debug.LogWarning("Kalan Hak 0!");
            _healthBarView.ShowGameState();
            return;
        }
        _healthBarView.UpdateHealthBar(cnt);
    }

    private void InitializeInputEvents() {
        //Daha az bagimlilik ile sagladi.
        _healthBarView.InitializeButtonEvents(this);
    }
}
