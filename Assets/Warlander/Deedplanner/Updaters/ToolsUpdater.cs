using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Warlander.Deedplanner.Data;
using Warlander.Deedplanner.Gui;
using Warlander.Deedplanner.Logic;

namespace Warlander.Deedplanner.Updaters
{
    public class ToolsUpdater : AbstractUpdater
    {
        [SerializeField] private Toggle calculateMaterialsToggle = null;
        [SerializeField] private Toggle mapWarningsToggle = null;
        
        [SerializeField] private RectTransform calculateMaterialsPanelTransform = null;
        [SerializeField] private RectTransform mapWarningsPanelTransform = null;

        [SerializeField] private RectTransform materialsWindowTransform = null;
        [SerializeField] private TMP_InputField materialsInputField = null;

        private ToolType currentTool = ToolType.MaterialsCalculator;
        
        private void OnEnable()
        {
            LayoutManager.Instance.TileSelectionMode = TileSelectionMode.Tiles;
        }

        public void OnModeChange(bool toggledOn)
        {
            if (!toggledOn)
            {
                return;
            }

            RefreshMode();
            RefreshGui();
        }

        private void RefreshMode()
        {
            if (calculateMaterialsToggle.isOn)
            {
                currentTool = ToolType.MaterialsCalculator;
            }
            else if (mapWarningsToggle.isOn)
            {
                currentTool = ToolType.MapWarnings;
            }
        }

        private void RefreshGui()
        {
            calculateMaterialsPanelTransform.gameObject.SetActive(currentTool == ToolType.MaterialsCalculator);
            mapWarningsPanelTransform.gameObject.SetActive(currentTool == ToolType.MapWarnings);
        }
        
        private void Update()
        {
            RaycastHit raycast = LayoutManager.Instance.CurrentCamera.CurrentRaycast;
            if (!raycast.transform)
            {
                return;
            }
            
            OverlayMesh overlayMesh = raycast.transform.GetComponent<OverlayMesh>();
            
            
        }

        public void CalculateMapMaterials()
        {
            Materials mapMaterials = GameManager.Instance.Map.CalculateMapMaterials();
            
            ShowMaterialsWindow(mapMaterials.ToString());
        }

        private void ShowMaterialsWindow(string text)
        {
            materialsWindowTransform.gameObject.SetActive(true);
            materialsInputField.text = text;
        }
        
        private enum ToolType
        {
            MaterialsCalculator, MapWarnings
        }
    }
}