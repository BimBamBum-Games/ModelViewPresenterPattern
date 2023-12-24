using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour {
    [SerializeField] RectTransform _horizontalLayoutHolderRct;
    [HideInInspector] public List<ElementOfHealthBar> healthPoints;
    [SerializeField] Button _takeDamageBtn, _resetHealthBarBtn;
    [SerializeField] Image _gameState_tmp;

    public void Start() {
        healthPoints = new List<ElementOfHealthBar>();
    }

    public void TakeDamageOnClick(Action act) {
        //Presenterin bu classa ait buttonlarina subscribe olmasi icin dusuk seviyeli bagimlilik amaciyla kendini bu classa gecer.
        _takeDamageBtn.onClick.AddListener(act.Invoke);
    }

    public void ResetHealthBarOnClick(Action act) {
        _resetHealthBarBtn.onClick.AddListener(act.Invoke);
    }

    public void AddHealthPoint(ElementOfHealthBar prefab, int quantity) {
        //Oyun baslarken Awake veya Start aninda initializasyon saglanir.
        healthPoints.Clear();
        for (int i = 0; i < quantity; i++) {
            ElementOfHealthBar eohb = Instantiate(prefab, _horizontalLayoutHolderRct);
            healthPoints.Add(eohb);
        }
    }

    public void UpdateHealthBar(int damagePoint) {
        //Presenter tarafindan cagrilir. Ekranda kalan haklarin sprite modelleri ile view guncellenecektir.
        int from = healthPoints.Count - 1;
        int to = damagePoint - 1;
        for (int i = from; i > to; i--) {
            ElementOfHealthBar eohb = healthPoints[i];
            if(i == 0) {
                eohb.AnimateWithOnComplete(EaseUtility.EaseInOutElastic, 1f, ShowGameState, false);
            }
            else {
                eohb.AnimateWithDefault(EaseUtility.EaseInOutElastic, 1f, false);
            }
        }
    }

    public void SetDisableEOHB(ElementOfHealthBar eohb) {
        //ElementOfHealthBar IEnumerator animasyonu bittiginde invoke edilecektir.
        eohb.GetChildRect().gameObject.SetActive(false);
    }

    public void ResetHealthBar() {
        //HealthBar GameObjelerini resetleyip tekrardan gorunur duruma getirir.
        int nd = healthPoints.Count - 1;
        while (nd > -1) {
            ElementOfHealthBar eohb = healthPoints[nd];
            eohb.SetOrResetTransformValues();
            eohb.AnimateWithDefault(EaseUtility.EaseInOutElastic, 1f, true);
            nd--;
        }
        //GameState bildirisi tmp komponentini gizler.
        HideGameState();
    }

    public void ShowGameState() {
        //Haklar bittiginde ekranda bir text ile bildir.
        Debug.Log("OnComplete!");
        _gameState_tmp.gameObject.SetActive(true);
    }

    public void HideGameState() {
        //GameStateyi Presenterda Awake veya Start esnasinda initialize et.
        _gameState_tmp.gameObject.SetActive(false);
    }
}
