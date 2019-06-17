using System;
using UnityEditor;
using UnityEngine;

namespace EditorModeModifications.Base
{
#if (UNITY_EDITOR)
    public class EditorModificator : MonoBehaviour
    {
        [HideInInspector]public string path = "Assets/Ressources/";

        public object LoadMyAsset(Type givenType ,string pathEnding)
        {
            return AssetDatabase.LoadAssetAtPath(path + pathEnding, givenType);
        }
    }
#endif
}

