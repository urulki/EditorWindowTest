using System;
using EditorModeModifications.Base;
using UnityEngine;

namespace EditorModeModifications.Grid
{
#if (UNITY_EDITOR)
    [ExecuteInEditMode]
    public class CellMatSetter : EditorModificator
    {
        private GameObject currentCell;
        
        private Vector3 RotValue = new Vector3(0,90,0);
        
        private Material StrRoad;
        private Material LTRoad;
        private Material RTRoad;
        private Material FourRoad;
        private Material ThreeRoad;
        private Material Empty;
        public enum CellTypes 
        {
            HorRoad,
            VertRoad,
            LeftToBotRoad,
            LeftToTopRoad,
            RightToBotRoad,
            RightToTopRoad,
            FourRoad,
            ThreeTopRoad,
            ThreeRightRoad,
            ThreeBotRoad,
            ThreeLeftRoad,
            Empty
        };
        

        public CellTypes RoadType;

        private CellTypes Checker;
        
        private void SetMaterials()
        {
            Empty = LoadMyAsset(typeof(Material),"Materials/Grid/Empty.mat") as Material;
            
            StrRoad = LoadMyAsset(typeof(Material),"Materials/Grid/StrRoad.mat") as Material;
            
            LTRoad = LoadMyAsset(typeof(Material),"Materials/Grid/LURoad.mat") as Material;
            
            RTRoad = LoadMyAsset(typeof(Material),"Materials/Grid/RURoad.mat") as Material;
            
            ThreeRoad = LoadMyAsset(typeof(Material),"Materials/Grid/3RoadUp.mat") as Material;
            
            FourRoad = LoadMyAsset(typeof(Material),"Materials/Grid/4Road.mat") as Material;
        }

        private void Awake()
        {
            currentCell = gameObject;
            SetMaterials();
            UpdateMaterial();
            Checker = RoadType;
        }

        private void Update()
        {
            if (RoadType != Checker) UpdateMaterial();
            if (Checker != CellTypes.Empty) gameObject.isStatic = true;
        }

        private void UpdateMaterial()
        {
            switch (RoadType)
            {
                case CellTypes.Empty :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = Empty;
                    currentCell.transform.localEulerAngles = Vector3.zero;
                    gameObject.isStatic = false;
                    Checker = RoadType;
                    break;
                case CellTypes.FourRoad :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = FourRoad;
                    currentCell.transform.localEulerAngles = Vector3.zero;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.HorRoad :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = StrRoad;
                    currentCell.transform.localEulerAngles = Vector3.zero;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.VertRoad :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = StrRoad;
                    currentCell.transform.localEulerAngles = RotValue;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.ThreeBotRoad :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = ThreeRoad;
                    currentCell.transform.localEulerAngles = RotValue*2;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.ThreeLeftRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = ThreeRoad;
                    currentCell.transform.localEulerAngles = -RotValue;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.ThreeRightRoad :
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = ThreeRoad;
                    currentCell.transform.localEulerAngles = RotValue;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.ThreeTopRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = ThreeRoad;
                    currentCell.transform.localEulerAngles = Vector3.zero;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.LeftToBotRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = LTRoad;
                    currentCell.transform.localEulerAngles = RotValue*2;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.LeftToTopRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = LTRoad;
                    currentCell.transform.localEulerAngles = -RotValue;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.RightToBotRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = RTRoad;
                    currentCell.transform.localEulerAngles = RotValue*2;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
                case CellTypes.RightToTopRoad : 
                    currentCell.GetComponent<MeshRenderer>().sharedMaterial = RTRoad;
                    currentCell.transform.localEulerAngles = RotValue;
                    gameObject.isStatic = true;
                    Checker = RoadType;
                    break;
            }
        }
    }
#endif
}

