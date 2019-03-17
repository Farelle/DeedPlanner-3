﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Warlander.Deedplanner.Logic;

namespace Warlander.Deedplanner.Data
{
    public class Ground : MonoBehaviour
    {

        public GroundData Data { get; private set; }
        public RoadDirection RoadDirection { get; private set; }

        private MeshRenderer meshRenderer;
        public MeshCollider Collider { get; private set; }

        public void Initialize(GroundData data, Mesh mesh)
        {
            gameObject.layer = LayerMasks.GroundLayer;
            if (!meshRenderer)
            {
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            }
            if (!GetComponent<MeshFilter>())
            {
                gameObject.AddComponent<MeshFilter>();
            }

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            if (!Collider)
            {
                Collider = gameObject.AddComponent<MeshCollider>();
            }
            Collider.sharedMesh = mesh;

            Data = data;
            RoadDirection = RoadDirection.Center;
            Material[] materials = new Material[4];
            for (int i = 0; i < 4; i++)
            {
                materials[i] = Data.Tex3d.Material;
            }
            meshRenderer.materials = materials;
        }

        public void SetData(GroundData data, SurfaceTile callingTile)
        {
            Data = data;
            SetRoadDirection(RoadDirection, callingTile);
        }

        public void SetRoadDirection(RoadDirection direction, SurfaceTile callingTile)
        {
            RoadDirection = direction;

            Material[] materials = new Material[4];
            if (direction == RoadDirection.Center)
            {
                for (int i = 0; i < 4; i++)
                {
                    materials[i] = Data.Tex3d.Material;
                }
            }
            else
            {
                Material matW = GameManager.Instance.Map[callingTile.X - 1, callingTile.Y]?.Surface.Ground.Data.Tex3d.Material;
                Material matE = GameManager.Instance.Map[callingTile.X + 1, callingTile.Y]?.Surface.Ground.Data.Tex3d.Material;
                Material matS = GameManager.Instance.Map[callingTile.X, callingTile.Y - 1]?.Surface.Ground.Data.Tex3d.Material;
                Material matN = GameManager.Instance.Map[callingTile.X, callingTile.Y + 1]?.Surface.Ground.Data.Tex3d.Material;

                if (direction == RoadDirection.NW || direction == RoadDirection.SW || !matW)
                {
                    matW = Data.Tex3d.Material;
                }
                if (direction == RoadDirection.NE || direction == RoadDirection.SE || !matE)
                {
                    matE = Data.Tex3d.Material;
                }
                if (direction == RoadDirection.SW || direction == RoadDirection.SE || !matS)
                {
                    matS = Data.Tex3d.Material;
                }
                if (direction == RoadDirection.NW || direction == RoadDirection.NE || !matN)
                {
                    matN = Data.Tex3d.Material;
                }

                materials[0] = matW;
                materials[1] = matN;
                materials[2] = matE;
                materials[3] = matS;
            }

            meshRenderer.materials = materials;
        }

    }
}
