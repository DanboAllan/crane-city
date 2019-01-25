// =====================================================================
// Copyright 2013-2017 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

#if UNITY_5_4 || UNITY_5_3 || UNITY_5_2 || UNITY_5_1 || UNITY_5_0
#define UNITY_PRE_5_5
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Components
{
    /// <summary>
    /// Class to drive a LineRenderer with a CurvySpline
    /// </summary>
    [AddComponentMenu("Curvy/Misc/Curvy Line Renderer")]
    [RequireComponent(typeof(LineRenderer))]
    [ExecuteInEditMode]
    [HelpURL(CurvySpline.DOCLINK + "curvylinerenderer")]
    public class CurvyLineRenderer : MonoBehaviour
    {
        public CurvySplineBase m_Spline;

        public CurvySplineBase Spline
        {
            get { return m_Spline; }
            set
            {
                if (m_Spline != value)
                {
                    unbindEvents();
                    m_Spline = value;
                    bindEvents();
                    Refresh();
                }
            }
        }

        LineRenderer mRenderer;

        void Awake()
        {
            mRenderer = GetComponent<LineRenderer>();
            m_Spline = GetComponent<CurvySpline>();
            if (!m_Spline)
                m_Spline = GetComponent<CurvySplineGroup>();
        }

        void OnEnable() 
        {
            mRenderer = GetComponent<LineRenderer>();
            bindEvents();
        }

        void OnDisable()
        {
            unbindEvents();
        }

        IEnumerator Start() 
        {
            if (Spline!=null)
                while (!Spline.IsInitialized)
                    yield return 0;

            Refresh();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            Refresh();
        }
#endif

        public void Refresh()
        {
            if (Spline && Spline.IsInitialized)
            {
                var vts = Spline.GetApproximation();
#if UNITY_PRE_5_5
                mRenderer.SetVertexCount(vts.Length);
                for (int v = 0; v < vts.Length; v++)
                    mRenderer.SetPosition(v, vts[v]);
#else
                mRenderer.numPositions = vts.Length;
                mRenderer.SetPositions(vts);
#endif
                
                
            }
            else if (mRenderer != null)
            {
#if UNITY_PRE_5_5
                mRenderer.SetVertexCount(0);
#else
                mRenderer.numPositions = 0;
#endif
            }
        }

        void OnSplineRefresh(CurvySplineEventArgs e)
        {
            Refresh();
        }

        void bindEvents() 
        {
            if (Spline)
                Spline.OnRefresh.AddListenerOnce(OnSplineRefresh);
        }

        void unbindEvents() 
        {
            if (Spline)
                Spline.OnRefresh.RemoveListener(OnSplineRefresh);
        }

    }
}
