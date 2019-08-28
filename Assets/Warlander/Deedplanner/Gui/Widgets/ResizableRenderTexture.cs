﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Warlander.Deedplanner.Gui.Widgets
{
    
    [RequireComponent(typeof(RawImage))]
    [RequireComponent(typeof(RectTransform))]
    public class ResizableRenderTexture : MonoBehaviour
    {

        public Camera renderCamera;

        private void Start()
        {
            OnRectTransformDimensionsChange();
        }

        private void OnRectTransformDimensionsChange()
        {
            RectTransform rectTransform = transform as RectTransform;
            Vector2 size = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform).size;
            RawImage rawImage = GetComponent<RawImage>();
            Vector3 scale = transform.lossyScale;
            float width = size.x * scale.x;
            float height = size.y * scale.y;
            if (width <= 0 || height <= 0)
            {
                return;
            }

            Texture oldTexture = rawImage.texture;
            if (oldTexture && Math.Abs(oldTexture.width - width) > 1 && Math.Abs(oldTexture.height - height) > 1)
            {
                Destroy(oldTexture);
                RenderTexture renderTexture = new RenderTexture((int) width, (int) height, 16, RenderTextureFormat.ARGB32);
                rawImage.texture = renderTexture;
            }
            else if (!oldTexture)
            {
                RenderTexture renderTexture = new RenderTexture((int) width, (int) height, 16, RenderTextureFormat.ARGB32);
                rawImage.texture = renderTexture;
            }

            if (renderCamera)
            {
                renderCamera.targetTexture = (RenderTexture) rawImage.texture;
                renderCamera.aspect = width / height;
            }
        }

    }

}