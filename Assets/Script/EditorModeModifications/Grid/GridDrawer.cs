using System;
using EditorModeModifications.Base;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace EditorModeModifications.Grid
{
#if (UNITY_EDITOR)
    [ExecuteInEditMode]
    public class GridDrawer : EditorModificator
    {
        private Transform _gridHandler;
        private GameObject _tilePreset;

        [Header("Saving")] 
        public bool Save;
        [SerializeField] private bool succed;
        
        
        [Header("Map Parameters")]
        public int Size;

        public int Multiplicator;
        private int multi;
        
        public int SizeMult
        {
            get { return multi; }
            set
            {
                if (multi == SizeMult) return;
                multi = SizeMult;
                transform.localScale = new Vector3(SizeMult,0,SizeMult);
            }
        }
        private int lastMapSize;
        public int MapSize
        {
            get { return Size;}
            set
            {
                if (lastMapSize == value) return;
                lastMapSize = value;
               
            }
        }
        

        private int tileScale;
        
        private void VariablesSetter()
        {
            _gridHandler = transform;
            _tilePreset = LoadMyAsset(typeof(GameObject), "Prefabs/Grid/Cell.prefab") as GameObject;
            tileScale = (int) _tilePreset.transform.localScale.x;
            
        }
        
        private void Awake()
        {
            VariablesSetter();
        }

        private void Update()
        {
            SizeMult = Multiplicator;
            MapSize = Size;
            if (Save)
            {
                
                Save = false;
            }
        }

        

        
    }
#endif
}
