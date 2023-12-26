using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviourBase {
    [SerializeField] RectTransform _horizontalLayoutHolderRct;
    [HideInInspector] public List<ElementOfHealthBar> healthPoints;
    [field: SerializeField] public Button TakeDamageBtn { get; private set; }
    [field: SerializeField] public Button ResetHealthBarBtn { get; private set; }
    [field: SerializeField] public Button RecreateHealthBarBtn { get; private set; }

    [SerializeField] Image _gameState_tmp;

    [Header("ElementTmpOfView Iceren TMpro Elementi")]
    [SerializeField] ElementTmpOfView  _healthBarIndicator;

    public void Start() {
        healthPoints = new List<ElementOfHealthBar>();
    }

    public void AddHealthPoint(ElementOfHealthBar prefab, int quantity) {
        //Oyun baslarken Awake veya Start aninda initializasyon saglanir.
        DestroyElementsOfHealthBar();
        for (int i = 0; i < quantity; i++) {
            ElementOfHealthBar eohb = Instantiate(prefab, _horizontalLayoutHolderRct);
            healthPoints.Add(eohb);
        }
        UpdateHealthPointIndicator(quantity.ToString());
    }

    private void DestroyElementsOfHealthBar() {
        //Tum ElementOfHealthBar prefab orneklerini destroy eder.
        for(int i = 0; i < healthPoints.Count; i++) {
            Destroy(healthPoints[i].gameObject);
            Dlog("Test : Destroy cagrildi!");
        }
        healthPoints.Clear();
    }

    public void UpdateHealthBar(int damagePoint) {
        //Presenter tarafindan cagrilir. Ekranda kalan haklarin sprite modelleri ile view guncellenecektir.
        int from = healthPoints.Count - 1;
        int to = damagePoint - 1;
        for (int i = from; i > to; i--) {
            ElementOfHealthBar eohb = healthPoints[i];
            if(i == 0) {
                eohb.AnimateChildWithOnComplete(EaseUtility.EaseInOutElastic, 0.6f, ShowGameState, false);      
            }
            else {
                eohb.AnimateChild(EaseUtility.EaseInOutElastic, 0.6f, false);
                _healthBarIndicator.AnimateItself();
            }
        }
        UpdateHealthPointIndicator(damagePoint.ToString());
    }

    public void ResetHealthBar() {
        //HealthBar GameObjelerini resetleyip tekrardan gorunur duruma getirir.
        int nd = healthPoints.Count;
        UpdateHealthPointIndicator(nd.ToString());
        //Liste elemen sayisi liste indexi disina ciktigindan dolayi sayi bir azaltilir.
        nd--;
        while (nd > -1) {
            ElementOfHealthBar eohb = healthPoints[nd];
            eohb.SetOrResetTransformValues();
            eohb.AnimateChild(EaseUtility.EaseInOutElastic, 1f, true);
            nd--;
        }
        //GameState bildirisi tmp komponentini gizler.
        HideGameState();
    }

    public void ShowGameState() {
        //Haklar bittiginde ekranda bir text ile bildir.
        Dlog("Test : OnComplete!");
        _gameState_tmp.gameObject.SetActive(true);
    }

    public void HideGameState() {
        //GameStateyi Presenterda Awake veya Start esnasinda initialize et.
        _gameState_tmp.gameObject.SetActive(false);
    }

    public void UpdateHealthPointIndicator(string hpValue) {
        if (_healthBarIndicator != null) {
            _healthBarIndicator.TmpGui.text = hpValue;
        }
        else {
            Dlog("TmpGUI elementi referansi null degerindedir!");
        }
    }
}
