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
    private ReorderableList _spellsList;

    private void OnEnable()
    {
        _card = (Card)target;

        _spellsList = new ReorderableList(serializedObject, serializedObject.FindProperty("Spells"));

        _spellsList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Spells");
        };


        _spellsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            EditorGUI.PropertyField(rect, serializedObject.FindProperty("Spells").GetArrayElementAtIndex(index));
        };


    }

    public override void OnInspectorGUI()
    {
     
        _card.CardName = EditorGUILayout.TextField(_card.CardName);
        //_card.Priority = EditorGUILayout.IntField(_card.Priority, GUILayout.Width(35));

        _card.Image = (Sprite)EditorGUILayout.ObjectField(_card.Image, typeof(Sprite), false, GUILayout.Width(150), GUILayout.Height(150));
        
 
        _card.CardType = (CardStats.CardType)EditorGUILayout.EnumPopup("CardType", _card.CardType);
        _card.Rarity = (CardStats.Rarity)EditorGUILayout.EnumPopup("Rarity", _card.Rarity);
        _card.description = EditorGUILayout.TextArea(_card.description, GUILayout.Height(50));

        if (_card.CardType == CardStats.CardType.Class)
        {
            _card.Battler = (BattlerClass)EditorGUILayout.ObjectField("Class", _card.Battler, typeof(BattlerClass), false);
        }

        _spellsList.DoLayoutList();

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
