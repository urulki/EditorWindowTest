using System;
using EditorModeModifications.Grid;
using UnityEditor;
using UnityEngine;

namespace Script.EditorModeModifications.WinEditor
{
    public class CellSetterWindows : EditorWindow
    {
        public GameObject CellToModif;
        public GameObject currentCell;
        
        
        [MenuItem("Tool/3DTilling/Cell Setter")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            CellSetterWindows window = (CellSetterWindows) GetWindow(typeof(CellSetterWindows),true,"Cell Setter" );
            window.Show();
            
        }

        private void OnGUI()
        {
            CellToModif = (GameObject) EditorGUILayout.ObjectField("Current Tile", CellToModif, typeof(GameObject),true);
            Selection.selectionChanged += () => setActiveTile();
        }

        void setActiveTile()
        {
            if(Selection.activeGameObject != null)
            {
                GameObject cGo = Selection.activeGameObject;
                if (cGo.GetComponent<CellMatSetter>() != null) CellToModif = cGo; 
                currentCell = CellToModif;
            }
        }
    }
}
