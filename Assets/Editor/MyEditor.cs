using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Example))]
public class MyEditor : Editor
{
    private SerializedProperty serializedValue;
    private SerializedProperty serializedColor;

    private void OnEnable()
    {
        serializedValue = serializedObject.FindProperty("value");
        serializedColor = serializedObject.FindProperty("color");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedValue.floatValue = EditorGUILayout.Slider("Custom Value", serializedValue.floatValue,
            -1, 1);
        serializedColor.colorValue = EditorGUILayout.ColorField("Custom Color", serializedColor.colorValue);
        
        Rect someRect = GUILayoutUtility.GetRect(0, 50);
        Rect linePosition = new Rect(someRect.x, someRect.y, someRect.width, 2);
        EditorGUI.DrawRect(linePosition, serializedColor.colorValue);

        //GUILayout.Space(30);
        
        if(GUILayout.Button("Change Color"))
        {
            serializedValue.floatValue = Random.Range(-1.0f, 1.0f);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
