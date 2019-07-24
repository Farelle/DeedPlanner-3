﻿using System.Text;
using System.Xml;
using UnityEngine;
using Warlander.Deedplanner.Logic;
using Warlander.Deedplanner.Utils;

namespace Warlander.Deedplanner.Data.Grounds
{
    public class Ground : TileEntity
    {

        private Tile tile;
        private GroundData data;
        private RoadDirection roadDirection = RoadDirection.Center;

        private MeshRenderer meshRenderer;
        public MeshCollider Collider { get; private set; }

        public override Tile Tile => tile;
        public override Materials Materials => null;

        public GroundData Data {
            get => data;
            set => tile.Map.CommandManager.AddToActionAndExecute(new GroundDataChangeCommand(this, data, value));
        }

        public RoadDirection RoadDirection {
            get => roadDirection;
            set => tile.Map.CommandManager.AddToActionAndExecute(new RoadDirectionChangeCommand(this, roadDirection, value));
        }

        public void Initialize(Tile tile, GroundData data, Mesh mesh)
        {
            this.tile = tile;

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
            meshFilter.sharedMesh = mesh;

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
            meshRenderer.sharedMaterials = materials;
        }

        private void RefreshAllState()
        {
            RefreshState();
            tile.Map[tile.X - 1, tile.Y]?.Ground.RefreshState();
            tile.Map[tile.X + 1, tile.Y]?.Ground.RefreshState();
            tile.Map[tile.X, tile.Y - 1]?.Ground.RefreshState();
            tile.Map[tile.X, tile.Y + 1]?.Ground.RefreshState();
        }
        
        private void RefreshState()
        {
            Material[] materials = new Material[4];
            if (roadDirection == RoadDirection.Center)
            {
                for (int i = 0; i < 4; i++)
                {
                    materials[i] = Data.Tex3d.Material;
                }
            }
            else
            {
                Material matW = GameManager.Instance.Map.GetRelativeTile(tile, -1, 0)?.Ground.Data.Tex3d.Material;
                Material matE = GameManager.Instance.Map.GetRelativeTile(tile, 1, 0)?.Ground.Data.Tex3d.Material;
                Material matS = GameManager.Instance.Map.GetRelativeTile(tile, 0, -1)?.Ground.Data.Tex3d.Material;
                Material matN = GameManager.Instance.Map.GetRelativeTile(tile, 0, 1)?.Ground.Data.Tex3d.Material;

                if (roadDirection == RoadDirection.NW || roadDirection == RoadDirection.SW || !matW)
                {
                    matW = Data.Tex3d.Material;
                }
                if (roadDirection == RoadDirection.NE || roadDirection == RoadDirection.SE || !matE)
                {
                    matE = Data.Tex3d.Material;
                }
                if (roadDirection == RoadDirection.SW || roadDirection == RoadDirection.SE || !matS)
                {
                    matS = Data.Tex3d.Material;
                }
                if (roadDirection == RoadDirection.NW || roadDirection == RoadDirection.NE || !matN)
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

        public override void Serialize(XmlDocument document, XmlElement localRoot)
        {
            localRoot.SetAttribute("id", data.ShortName);
            if (roadDirection != RoadDirection.Center) {
                localRoot.SetAttribute("dir", roadDirection.ToString().ToUpperInvariant());
            }
        }

        public void Deserialize(XmlElement element)
        {
            string id = element.GetAttribute("id");
            string dir = element.GetAttribute("dir");

            Data = Database.Grounds[id];
            switch (dir)
            {
                case "NW":
                    RoadDirection = RoadDirection.NW;
                    break;
                case "NE":
                    RoadDirection = RoadDirection.NE;
                    break;
                case "SW":
                    RoadDirection = RoadDirection.SW;
                    break;
                case "SE":
                    RoadDirection = RoadDirection.SE;
                    break;
                default:
                    RoadDirection = RoadDirection.Center;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();

            build.Append("X: ").Append(tile.X).Append(" Y: ").Append(tile.Y).AppendLine();
            build.Append(data.Name);
            
            return build.ToString();
        }

        private class GroundDataChangeCommand : IReversibleCommand
        {

            private Ground ground;
            private GroundData oldData;
            private GroundData newData;
            
            public GroundDataChangeCommand(Ground ground, GroundData oldData, GroundData newData)
            {
                this.ground = ground;
                this.oldData = oldData;
                this.newData = newData;
            }
            
            public void Execute()
            {
                if (newData)
                {
                    ground.data = newData;
                    ground.RefreshAllState();
                }
            }

            public void Undo()
            {
                if (oldData)
                {
                    ground.data = oldData;
                    ground.RefreshAllState();
                }
            }

            public void DisposeUndo()
            {
                // no operation needed
            }

            public void DisposeRedo()
            {
                // no operation needed
            }
        }
        
        private class RoadDirectionChangeCommand : IReversibleCommand
        {

            private Ground ground;
            private RoadDirection oldDirection;
            private RoadDirection newDirection;
            
            public RoadDirectionChangeCommand(Ground ground, RoadDirection oldDirection, RoadDirection newDirection)
            {
                this.ground = ground;
                this.oldDirection = oldDirection;
                this.newDirection = newDirection;
            }
            
            public void Execute()
            {
                ground.roadDirection = newDirection;
                ground.RefreshAllState();
            }

            public void Undo()
            {
                ground.roadDirection = oldDirection;
                ground.RefreshAllState();
            }

            public void DisposeUndo()
            {
                // no operation needed
            }

            public void DisposeRedo()
            {
                // no operation needed
            }
        }

    }
}
