using System;
using UnityEngine;
using UnityEngine.Assertions;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 配列などの要素にenumの名前をラベルとして表示するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class EnumLabelAttribute : PropertyAttribute
{
    // enumに対応する名前一覧
    public string[] EnumNames { get; private set; }

    /// <summary>
    /// enumから名前一覧を取得
    /// </summary>
    /// <param name="enumType"></param>
    public EnumLabelAttribute(Type enumType)
    {
        Assert.IsTrue(enumType.IsEnum, "[EnumLabel] type of attribute parameter is not enum.");
        EnumNames = Enum.GetNames(enumType);
    }
}

#if UNITY_EDITOR
/// <summary>
/// EnumLabelAttributeが付与された配列等に対して、
/// Inspectorでの表示がenumになるようにするためのクラス
/// </summary>
[CustomPropertyDrawer(typeof(EnumLabelAttribute))]
public class EnumLabelDrawer : PropertyDrawer
{
    // "Element 0"のような表示からindexを取得するための正規表現
    private static readonly Regex elementRegex = new Regex(@"Element\s*(\d+)", RegexOptions.Compiled);

    /// <summary>
    /// Inspectorに表示
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 属性データを取得
        EnumLabelAttribute attr = (EnumLabelAttribute)attribute;
        // ラベルテキストから"Element 0"のような表記を探す
        Match match = elementRegex.Match(label.text);

        // ラベルにindexが含まれている場合
        if(match.Success && int.TryParse(match.Groups[1].Value, out int index))
        {
            // indexがenumの要素数より小さい場合
            if(index < attr.EnumNames.Length)
            {
                GUIContent displayLabel = new GUIContent(attr.EnumNames[index]);
                EditorGUI.PropertyField(position, property, displayLabel, true);
                return;
            }
            else
            {
                Debug.LogWarning($"[EnumLabel] 配列要素数がEnum数({attr.EnumNames.Length})より多くなっています。Index: {index}");
            }
        }

        // 通常の表示
        EditorGUI.PropertyField(position, property, label, true);
    }

    /// <summary>
    /// プロパティの高さを返す
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}
#endif