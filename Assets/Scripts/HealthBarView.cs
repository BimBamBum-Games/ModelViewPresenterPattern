using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour {
    [SerializeField] RectTransform _parentHolder;
    [HideInInspector] public List<GameObject> _healthPoints;
    [SerializeField] Button _takeDamageBtn;
    [SerializeField] Image _gameState_tmp;

    public void Start() {
        _healthPoints = new List<GameObject>();
    }

    public void InitializeButtonEvents(HealthBarPresenter hbp) {
        //Presenterin bu classa ait buttonlarina subscribe olmasi icin dusuk seviyeli bagimlilik amaciyla kendini bu classa gecer.
        _takeDamageBtn.onClick.AddListener(hbp.TakeDamageOnClick);
    }

    public void AddHealthPoint(Image prefabImg, int quantity) {
        _healthPoints.Clear();
        for (int i = 0; i < quantity; i++) {
            Image im = Instantiate(prefabImg, _parentHolder);
            _healthPoints.Add(im.gameObject);
        }
    }

    public void UpdateHealthBar(int damagePoint) {
        int from = _healthPoints.Count - 1;
        int to = damagePoint - 1;
        for (int i = from; i > to; i--) {
            _healthPoints[i].SetActive(false);
        }
    }

    public void ShowGameState() {
        //Haklar bittiginde ekranda bir text ile bildir.
        _gameState_tmp.gameObject.SetActive(true);
    }

    public void HideGameState() {
        //GameStateyi Presenterda Awake veya Start esnasinda initialize et.
        _gameState_tmp.gameObject.SetActive(false);
    }

}
