﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoordinateMapper {
    public class DrawGraticuleLine : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;

        [HideInInspector] public int segments;
        [HideInInspector] public float angle;
        [HideInInspector] public bool isLatitude;

        // Start is called before the first frame update
        void Start() {
            lineRenderer.positionCount = segments;
            lineRenderer.startWidth = 0.001f;
            lineRenderer.endWidth = 0.001f;
            lineRenderer.useWorldSpace = true;
            lineRenderer.loop = true;
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            DrawLine();
        }

        void DrawLine() {
            int segmentIncrement = 360 / segments;
            int currSegment = 0;
            for (int j = 0; j < 360; j += segmentIncrement) {

                var line = PlanetUtility.VectorFromLatLng(isLatitude ? angle : j, isLatitude ? j : angle, Vector3.right);

                var hitInfo = PlanetUtility.LineFromOriginToSurface(transform, line, LayerMask.GetMask("Planet"));
                if (hitInfo.HasValue) {
                    lineRenderer.SetPosition(currSegment, hitInfo.Value.point);
                }

                currSegment += 1;
            }
        }
    }
}