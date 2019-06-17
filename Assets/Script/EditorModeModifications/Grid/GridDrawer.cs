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
                DrawGrid(lastMapSize);
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
                SaveAsPrefab(gameObject);
                Save = false;
            }
        }

        void DrawGrid(int size)
        {
            foreach (var cellSetter in _gridHandler.GetComponentsInChildren<CellMatSetter>())
            {
                DestroyImmediate(cellSetter.gameObject);
            }
            if (size % 2 != 0) size--;
            int scaledSize = size * tileScale*2;
            for (int j = -scaledSize/2; j <= scaledSize/2; j+=tileScale*2)
            {
                for (int i = - scaledSize/2; i <= scaledSize/2; i+=tileScale*2)
                {
                    Vector3 tilePos = new Vector3(i,_gridHandler.position.y,j);
                    GameObject go = Instantiate(_tilePreset, tilePos, Quaternion.identity, _gridHandler);
                    go.name = _tilePreset.name + " ("+go.transform.localPosition.x+","+go.transform.localPosition.z+")";
                }
            }
        }

        void SaveAsPrefab(GameObject go)
        {
            

            foreach (var setter in go.GetComponentsInChildren<CellMatSetter>())
            {
                DestroyImmediate(setter);
            }

            GameObject prefab = Instantiate(go, Vector3.zero, Quaternion.identity);
            prefab.name = go.name + " Prefab";
            string localPath = path + "Prefabs/Grid/" + prefab.name + ".prefab";
            DestroyImmediate(prefab.GetComponent<GridDrawer>());
            PrefabUtility.SaveAsPrefabAsset(prefab, localPath,out succed);
            DestroyImmediate(prefab);
            succed = false;
        }
    }
#endif
}
