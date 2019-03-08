﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Warlander.Deedplanner.Utils;

namespace Warlander.Deedplanner.Data
{
    public class WallData
    {

        public Model BottomModel { get; private set; }
        public Model NormalModel { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public Color Color { get; private set; }
        public float Scale { get; private set; }
        public bool HouseWall { get; private set; }
        public bool Arch { get; private set; }
        public bool ArchBuildable { get; private set; }
    
        public TextureReference Icon { get; private set; }

        public Materials Materials { get; private set; }

        public WallData(Model bottomModel, Model normalModel, string name, string shortName, Color color, float scale, bool houseWall, bool arch, bool archBuildable, Materials materials, TextureReference icon)
        {
            BottomModel = bottomModel;
            NormalModel = normalModel;
            Name = name;
            ShortName = shortName;
            Color = color;
            Scale = scale;
            HouseWall = houseWall;
            Arch = arch;
            ArchBuildable = archBuildable;
            Icon = icon;
            if (materials != null)
            {
                Materials = materials;
            }
            else
            {
                Materials = new Materials();
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
