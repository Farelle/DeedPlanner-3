﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using Warlander.Deedplanner.Utils;

namespace Warlander.Deedplanner.Data
{

    public abstract class TileEntity : MonoBehaviour, IXMLSerializable
    {

        public abstract Materials Materials { get; }

        public abstract void Serialize(XmlDocument document, XmlElement localRoot);

    }

}
