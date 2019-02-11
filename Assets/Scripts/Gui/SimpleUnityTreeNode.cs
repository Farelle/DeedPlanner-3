﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Warlander.Deedplanner.Gui
{
    public class SimpleUnityTreeNode : UnityTreeNode
    {

        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private Toggle toggle;
        [SerializeField]
        private Image expandButtonImage;
        [SerializeField]
        private Image collapseButtonImage;

        private string value;

        public override string Value {
            get {
                return value;
            }
            set {
                this.value = value;
                text.SetText(value);
            }
        }

        public override List<UnityListElement> Leaves {
            get {
                List<UnityListElement> elements = new List<UnityListElement>();
                foreach (Transform childTransform in transform)
                {
                    if (childTransform.gameObject.GetComponent<UnityListElement>())
                    {
                        elements.Add(childTransform.gameObject.GetComponent<UnityListElement>());
                    }
                }
                return elements;
            }
        }

        public override List<UnityTreeNode> Branches {
            get {
                List<UnityTreeNode> elements = new List<UnityTreeNode>();
                foreach (Transform childTransform in transform)
                {
                    if (childTransform.gameObject.GetComponent<UnityTreeNode>())
                    {
                        elements.Add(childTransform.gameObject.GetComponent<UnityTreeNode>());
                    }
                }
                return elements;
            }
        }

        public void OnToggle()
        {
            expandButtonImage.gameObject.SetActive(!toggle.isOn);
            collapseButtonImage.gameObject.SetActive(toggle.isOn);
            foreach (Transform childTransform in transform)
            {
                if (childTransform.gameObject.GetComponent<UnityTreeNode>() != null || childTransform.gameObject.GetComponent<UnityListElement>() != null)
                {
                    childTransform.gameObject.SetActive(toggle.isOn);
                }
            }
        }

    }
}