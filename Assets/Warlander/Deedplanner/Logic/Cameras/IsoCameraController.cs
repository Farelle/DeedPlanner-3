﻿using UnityEngine;
using UnityEngine.EventSystems;
using Warlander.Deedplanner.Data;

namespace Warlander.Deedplanner.Logic.Cameras
{
    public class IsoCameraController : ICameraController
    {
        private Vector2 isoPosition;
        private float isoScale = 40;
        
        public bool SupportsMode(CameraMode mode)
        {
            return mode == CameraMode.Isometric;
        }

        public void UpdateDrag(PointerEventData eventData)
        {
            isoPosition += new Vector2(-eventData.delta.x * Properties.Instance.IsoMouseSensitivity, -eventData.delta.y * Properties.Instance.IsoMouseSensitivity);
        }

        public void UpdateInput(Map map, CameraMode mode, Vector3 focusedPoint, float aspect, int currentFloor, bool focusedWindow, bool mouseOver)
        {
            if (focusedWindow)
            {
                if (mouseOver)
                {
                    float scroll = Input.mouseScrollDelta.y;
                    if (scroll > 0 && isoScale > 10)
                    {
                        isoScale -= 4;
                    }
                    else if (scroll < 0)
                    {
                        isoScale += 4;
                    }
                }

                Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                movement *= Properties.Instance.IsoMovementSpeed * Time.deltaTime;
                isoPosition += movement;
            }

            if (isoPosition.x < -(map.Width * 4 / Mathf.Sqrt(2) - isoScale * aspect))
            {
                isoPosition.x = -(map.Width * 4 / Mathf.Sqrt(2) - isoScale * aspect);
            }
            if (isoPosition.y < isoScale)
            {
                isoPosition.y = isoScale;
            }

            if (isoPosition.x > map.Width * 4 / Mathf.Sqrt(2) - isoScale * aspect)
            {
                isoPosition.x = map.Width * 4 / Mathf.Sqrt(2) - isoScale * aspect;
            }
            if (isoPosition.y > map.Height * 4 / Mathf.Sqrt(2) - isoScale)
            {
                isoPosition.y = map.Height * 4 / Mathf.Sqrt(2) - isoScale;
            }

            bool fitsHorizontally = map.Width * 2 * Mathf.Sqrt(2) < isoScale * aspect;
            bool fitsVertically = map.Height * 2 / Mathf.Sqrt(2) < isoScale;

            if (fitsHorizontally)
            {
                isoPosition.x = 0;
            }
            if (fitsVertically)
            {
                isoPosition.y = map.Height * 2 / Mathf.Sqrt(2);
            }
        }

        public void UpdateState(Camera camera, Transform cameraTransform, Transform cameraParentTransform)
        {
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.orthographic = true;
            camera.orthographicSize = isoScale;
            cameraTransform.localPosition = new Vector3(isoPosition.x, isoPosition.y, -10000);
            cameraTransform.localRotation = Quaternion.identity;
            cameraParentTransform.localRotation = Quaternion.Euler(30, 45, 0);
        }
        
        public Vector2 CalculateWaterTablePosition(Vector3 cameraPosition)
        {
            return new Vector2(isoPosition.x, isoPosition.y);
        }
    }
}