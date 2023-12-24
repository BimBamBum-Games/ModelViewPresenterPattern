using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour {
    [SerializeField] RectTransform _parentHolder;
    [HideInInspector] public List<ElementOfHealthBar> healthPoints;
    [SerializeField] Button _takeDamageBtn;
    [SerializeField] Image _gameState_tmp;

    public void Start() {
        healthPoints = new List<ElementOfHealthBar>();
    }

    public void InitializeButtonEvents(Action act) {
        //Presenterin bu classa ait buttonlarina subscribe olmasi icin dusuk seviyeli bagimlilik amaciyla kendini bu classa gecer.
        _takeDamageBtn.onClick.AddListener(act.Invoke);
    }

    public void AddHealthPoint(ElementOfHealthBar prefab, int quantity) {
        //Oyun baslarken Awake veya Start aninda initializasyon saglanir.
        healthPoints.Clear();
        for (int i = 0; i < quantity; i++) {
            ElementOfHealthBar eohb = Instantiate(prefab, _parentHolder);
            healthPoints.Add(eohb);
        }
    }

    public void UpdateHealthBar(int damagePoint) {
        //Presenter tarafindan cagrilir. Ekranda kalan haklarin sprite modelleri ile view guncellenecektir.
        int from = healthPoints.Count - 1;
        int to = damagePoint - 1;
        for (int i = from; i > to; i--) {
            if (healthPoints[i].isActiveAndEnabled == true) {
                healthPoints[i].AnimateHPAtTheEndOfLife();
            }        
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
