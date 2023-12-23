using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new HealthBarModel", menuName = "Health Bar Model")]
public class HealthBarModelSO : ScriptableObject {
    public Image _healthPointSpr;
    public int quantity;
}
