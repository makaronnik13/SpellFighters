using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellGame
{
    public class Player : Singleton<Player>
    {

        public int PlayerClass
        {
            get
            {
                return FindObjectOfType<ClassChooser>()._currentClass;
            }
        }
    }
}
