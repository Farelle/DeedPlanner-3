﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Warlander.Deedplanner.Data;
using Warlander.Deedplanner.Gui;

namespace Warlander.Deedplanner.Logic
{
    public class RoofUpdater : MonoBehaviour
    {

        private void OnEnable()
        {
            LayoutManager.Instance.TileSelectionMode = TileSelectionMode.Tiles;
        }

        private void Update()
        {
            RaycastHit raycast = LayoutManager.Instance.CurrentCamera.CurrentRaycast;
            if (!raycast.transform)
            {
                return;
            }

            GridTile gridTile = raycast.transform.GetComponent<GridTile>();
            TileEntity tileEntity = raycast.transform.GetComponent<TileEntity>();

            int floor = 0;
            int x = -1;
            int y = -1;
            if (tileEntity)
            {
                floor = tileEntity.Floor;
                x = tileEntity.Tile.X;
                y = tileEntity.Tile.Y;
            }
            else if (gridTile)
            {
                floor = LayoutManager.Instance.CurrentCamera.Floor;
                x = gridTile.X;
                y = gridTile.Y;
            }

            if (floor <= 0)
            {
                return;
            }

            if (Input.GetMouseButton(0))
            {
                RoofData data = GuiManager.Instance.RoofsList.SelectedValue as RoofData;
                GameManager.Instance.Map[x, y].SetRoof(data, floor);
            }
            else if (Input.GetMouseButton(1))
            {
                GameManager.Instance.Map[x, y].SetRoof(null, floor);
            }
        }

    }
}
