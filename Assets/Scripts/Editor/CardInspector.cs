using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

[CustomEditor(typeof(Card))]
public class CardInspector : Editor
{

    private Card _card;
    private ReorderableList _bonusesList;

    private void OnEnable()
    {
        _card = (Card)target;

        _bonusesList = new ReorderableList(serializedObject, serializedObject.FindProperty("BonusAtack"));

        _bonusesList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "bonuses");
        };

        _bonusesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
        
        };

    }

    public override void OnInspectorGUI()
    {
     
        _card.CardName = EditorGUILayout.TextField(_card.CardName);
        _card.Priority = EditorGUILayout.IntField(_card.Priority, GUILayout.Width(35));

        _card.Image = (Sprite)EditorGUILayout.ObjectField(_card.Image, typeof(Sprite), false, GUILayout.Width(150), GUILayout.Height(150));
        
 
        _card.CardType = (CardStats.CardType)EditorGUILayout.EnumPopup("CardType", _card.CardType);
        _card.Rarity = (CardStats.Rarity)EditorGUILayout.EnumPopup("Rarity", _card.Rarity);
        _card.description = EditorGUILayout.TextArea(_card.description, GUILayout.Height(50));

        if (_card.CardType == CardStats.CardType.Class)
        {
            _card.Battler = (BattlerClass)EditorGUILayout.ObjectField("Class", _card.Battler, typeof(BattlerClass), false);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_card);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }
}

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
    }
}
