using System.Collections.Generic;
using EditorModeModifications.Grid;
using UnityEditor;
using UnityEngine;

namespace Script.EditorModeModifications.WinEditor
{
    public class GridSetterWindows : EditorWindow
    {
        string prefabName = "Grid";
        bool groupEnabled;
        private float CellSize =1;
        private float cellX = 1;
        private float cellZ = 1;
        private float cellY = 0.2f;
        private int gridSize =1;
        private int multiplicator = 1;
        private Object cellType;
        private Object SaveDestinationFile;
        private bool Preview;
        private bool Clear;
        private bool Save;
        private static GameObject grid;
        private GameObject tile;
        private bool succed;
        private string targetpath;
        private List<CellMatSetter> cellSetter = new List<CellMatSetter>();

        // Add menu named "My Window" to the Window menu
        [MenuItem("Tool/3DTilling/Grid Setter")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GridSetterWindows window = (GridSetterWindows)GetWindow(typeof(GridSetterWindows),false,"Grid Setter" );
            window.Show();
            if(GameObject.Find("Grid") == null)grid = new GameObject("Grid");
        }
        

        void OnGUI()
        {
            GUILayout.Label("World Settings", EditorStyles.boldLabel);
            prefabName = EditorGUILayout.TextField("PrefabName", prefabName);
            gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
            cellType = EditorGUILayout.ObjectField("Cell Preset", cellType, typeof(GameObject), false);
            SaveDestinationFile =EditorGUILayout.ObjectField("Target Folder", SaveDestinationFile, typeof(Object), false);
            multiplicator =EditorGUILayout.IntField("Cell Size Multiplicator", multiplicator);
            
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            
            cellX = EditorGUILayout.Slider("Cell X scale", cellX, 0.1f, 5);
            cellY = EditorGUILayout.Slider("Cell Y scale", cellY, 0.1f, 5);
            cellZ = EditorGUILayout.Slider("Cell Z scale", cellZ, 0.1f, 5);
            
            EditorGUILayout.EndToggleGroup();

            Preview = GUILayout.Button("Preview Grid");
            Clear = GUILayout.Button("Clear Grid");
            Save = GUILayout.Button("Save Grid");
            if (multiplicator < 1) multiplicator = 1;
            if (cellType !=null && tile == null) tile = (GameObject)cellType;
            if (cellType !=null)
            {
                if(groupEnabled)tile.transform.localScale = new Vector3(cellX*multiplicator,cellY,cellZ*multiplicator);
                else tile.transform.localScale = new Vector3(multiplicator,0.2f,multiplicator);    
            }
            if (Preview && cellType !=null)
            {
                if (GameObject.Find("Grid")==null) grid = new GameObject("Grid");
                if(tile.GetComponent<CellMatSetter>() == null)tile.AddComponent<CellMatSetter>();
                Debug.Log(targetpath);
                DrawGrid(gridSize);
            }
            
            if (grid == null) grid = GameObject.Find("Grid");
            if (Clear) ClearGrid();
            if (SaveDestinationFile !=null && targetpath == null)
            {
                targetpath = AssetDatabase.GetAssetPath(SaveDestinationFile);
                
            }
            if(Save && SaveDestinationFile !=null) SaveAsPrefab(grid);
        }
        void DrawGrid(int size)
        {
            Debug.Log(grid);
            Debug.Log(groupEnabled);
            cellSetter.Clear();
            foreach (var cellSetter in grid.GetComponentsInChildren<CellMatSetter>())
            {
                DestroyImmediate(cellSetter.gameObject);
            }
            if (size % 2 != 0) size--;
            if (!groupEnabled)
            {
                float tileScale = CellSize;
                float scaledSize = size * tileScale*multiplicator;
                for (float j = -scaledSize/2; j <= scaledSize/2; j+=tileScale*multiplicator)
                {
                    for (float i = - scaledSize/2; i <= scaledSize/2; i+=tileScale*multiplicator)
                    {
                        Vector3 tilePos = new Vector3(i,grid.transform.position.y,j);
                        GameObject go = (GameObject) Instantiate(tile, tilePos, Quaternion.identity, grid.transform);
                        go.name = tile.name + " ("+go.transform.localPosition.x+","+go.transform.localPosition.z+")";
                        cellSetter.Add(go.GetComponent<CellMatSetter>());
                    }
                }
            }
            else
            {
                float tileScaleX = cellX;
                float tileScaleZ = cellZ;
                float scaledSizeX = size * tileScaleX*multiplicator;
                float scaledSizeZ = size * tileScaleZ*multiplicator;
                
                for (float j = -scaledSizeZ/2; j <= scaledSizeZ/2; j+=tileScaleZ*multiplicator)
                {
                    for (float i = - scaledSizeX/2; i <= scaledSizeX/2; i+=tileScaleX*multiplicator)
                    {
                        Vector3 tilePos = new Vector3(i,grid.transform.position.y,j);
                        GameObject go = Instantiate(tile, tilePos, Quaternion.identity, grid.transform);
                        go.name = tile.name + " ("+go.transform.localPosition.x+","+go.transform.localPosition.z+")";
                        cellSetter.Add(go.GetComponent<CellMatSetter>());
                    }
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
            prefab.name = prefabName;
            string localPath = targetpath+ "/" + prefab.name + ".prefab";
            DestroyImmediate(prefab.GetComponent<GridDrawer>());
            PrefabUtility.SaveAsPrefabAsset(prefab, localPath,out succed);
            DestroyImmediate(prefab);
            DestroyImmediate(go);
            succed = false;
        }

        void ClearGrid()
        {
            foreach (var cellSetter in grid.GetComponentsInChildren<CellMatSetter>())
            {
                DestroyImmediate(cellSetter.gameObject);
            }
        }
    }
}
