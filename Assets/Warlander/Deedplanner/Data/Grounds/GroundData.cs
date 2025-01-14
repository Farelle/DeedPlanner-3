﻿using UnityEngine;
using Warlander.Deedplanner.Graphics;

namespace Warlander.Deedplanner.Data.Grounds
{
    public class GroundData
    {
        public string Name { get; }
        public string ShortName { get; }

        public TextureReference Tex2d { get; }
        public TextureReference Tex3d { get; }

        public bool Diagonal { get; }

        public GroundData(string name, string shortName, TextureReference tex2d, TextureReference tex3d, bool diagonal)
        {
            Name = name;
            ShortName = shortName;
            Tex2d = tex2d;
            Tex3d = tex3d;
            Diagonal = diagonal;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
