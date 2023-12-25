using UnityEngine;

public class HealthBarPresenter : MonoBehaviourBase {
    [SerializeField] HealthBarModel _healthBarModel;
    [SerializeField] HealthBarView _healthBarView;
    [SerializeField] int _dealtDamagePoint = 1;

    private void Start() {
        InitializeViewerUI();      
    }
    private void OnEnable() {
        InitializeInputEvents();
    }
    private void OnDisable() {
        FinalizeInputEvents();
    }

    private void InitializeViewerUI() {
        _healthBarView.HideGameState();
        _healthBarView.AddHealthPoint(_healthBarModel.GetHPImage(), _healthBarModel.GetHPCount());
    }

    public void TakeDamageOnClick() {
        //Modeli guncelleyerek kalan haklarin sayisini ogren. Eger haklar sifir olursa oyun bitti yazisi yazdir.
        int cnt = _healthBarModel.UpdateHPCount(_dealtDamagePoint);
        int cmp = Mathf.Max(0, cnt);

        Dlog("Test : CMP >>>>> " + cmp);

        if (cmp >= 0) {
            _healthBarView.UpdateHealthBar(cmp);
        }
    }

    private void InitializeInputEvents() {
        //View Button GameObjelerine methodlari subscribe duruma getir.
        _healthBarView.TakeDamageBtn.onClick.AddListener(TakeDamageOnClick);
        _healthBarView.ResetHealthBarBtn.onClick.AddListener(ResetModelAndView);
        _healthBarView.RecreateHealthBarBtn.onClick.AddListener(RecreateView);
    }

    private void FinalizeInputEvents() {
        //Mem lack onlemek icin OnDisable veya Destroy aninda unsubscribe duruma getir.
        _healthBarView.TakeDamageBtn.onClick.RemoveListener(TakeDamageOnClick);
        _healthBarView.ResetHealthBarBtn.onClick.RemoveListener(ResetModelAndView);
        _healthBarView.RecreateHealthBarBtn.onClick.RemoveListener(RecreateView);
    }

    private void ResetModelAndView() {
        //Tum yapiyi resetler.
        _healthBarView.ResetHealthBar();
        _healthBarModel.ResetModel();
    }

    private void RecreateView() {
        //Tum yapiyi tekrardan insa eder.
        _healthBarModel.ResetModel();
        //_healthBarView.
        InitializeViewerUI();
    }
}
