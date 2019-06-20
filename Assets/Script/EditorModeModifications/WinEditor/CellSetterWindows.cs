using System;
using System.Collections.Generic;
using System.Linq;
using EditorModeModifications.Grid;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.EditorModeModifications.WinEditor
{
    public class CellSetterWindows : EditorWindow
    {
        public GameObject cellToModif;
        public GameObject currentCell;
        public Material baseMat;
        public List<Texture> Textures = new List<Texture>();
        public List<Material> materialInstances = new List<Material>();
        public Object TargetTextureFolder;

        [MenuItem("Tool/3DTilling/Cell Setter")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            CellSetterWindows window = (CellSetterWindows) GetWindow(typeof(CellSetterWindows),true,"Cell Setter" );
            window.Show();
            
        }

        private void OnGUI()
        {
            GUILayout.Label("Cell Settings",EditorStyles.boldLabel);
            TargetTextureFolder =
                EditorGUILayout.ObjectField("Texture to apply to materials", TargetTextureFolder, typeof(Object));
            cellToModif = (GameObject) EditorGUILayout.ObjectField("Current Tile", cellToModif, typeof(GameObject),true);
            baseMat = (Material) EditorGUILayout.ObjectField("Base Material", baseMat, typeof(Material), false);

            Selection.selectionChanged += () => SetActiveTile();
        }

        void SetActiveTile()
        {
            if(Selection.activeGameObject != null)
            {
                GameObject cGo = Selection.activeGameObject;
                if (cGo.GetComponent<CellMatSetter>() != null) cellToModif = cGo; 
                currentCell = cellToModif;
            }
        }

        void MatCreator()
        {
            foreach (var text in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(TargetTextureFolder)))
            {
                Textures.Add((Texture) text);
            }

            foreach (var texture in Textures)
            { 
                Material mat = new Material(baseMat);
                mat.mainTexture = texture;
                materialInstances.Add(mat);
            }
        }
    }
}