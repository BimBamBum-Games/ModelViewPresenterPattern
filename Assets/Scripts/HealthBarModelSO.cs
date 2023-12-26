using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "new HealthBarModel", menuName = "Health Bar Model")]
public class HealthBarModelSO : ScriptableObject {
    //Model ile dogrudan iliskili olarak prefabin sunulmasi icin olusturulmus SO sinifidir.
    public ElementOfHealthBar elementOfHealthBar;
    [HideInInspector] public int quantity;
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(HealthBarModelSO))]
public class HealthBarModelSOEditor : Editor {
    SerializedProperty _quantitySrp;
    private void OnEnable() {
        _quantitySrp = serializedObject.FindProperty(nameof(HealthBarModelSO.quantity));
    }
    public override void OnInspectorGUI() {
        CheckUpLowerQuantityLimit();
    }

    private void CheckUpLowerQuantityLimit() {
        //Quantity sayisal degerinin alt limitini kontrol eder.
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        _quantitySrp.intValue = EditorGUILayout.IntField("Min", _quantitySrp.intValue);
        if (EditorGUI.EndChangeCheck()) {
            if (_quantitySrp.intValue < 0) 
                _quantitySrp.intValue = 0;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif