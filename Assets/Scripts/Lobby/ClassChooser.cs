using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class ClassChooser : MonoBehaviour {

    private int __currentClass;
    public int _currentClass
    {
        get
        {
            return __currentClass;
        }
        set
        {
            __currentClass = value;

            if (value >= DefaultResources.AllClasses.Length)
            {
                __currentClass = value - DefaultResources.AllClasses.Length;
            }
            if (value<0)
            {
                __currentClass = DefaultResources.AllClasses.Length + value;
            }

            //update ui
            ClassName.text = DefaultResources.AllClasses[__currentClass].BattlerName;
            ClassHp.text = DefaultResources.AllClasses[__currentClass].Hp+"";
            ClassDescription.text = DefaultResources.AllClasses[__currentClass].Description;
            ClassImage.sprite = DefaultResources.AllClasses[__currentClass].BattlerImage;
        }
    }

    public TextMeshProUGUI ClassName;
    public TextMeshProUGUI ClassHp;
    public TextMeshProUGUI ClassDescription;
    public Image ClassImage;

    private void Start()
    {
        _currentClass = Random.Range(0, DefaultResources.AllClasses.Length);
    }

    public void SwapClass(int i)
    {
        _currentClass += i;
    }
}
