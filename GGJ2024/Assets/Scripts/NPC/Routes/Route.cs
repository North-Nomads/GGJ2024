using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPC.Routes
{
    public class Route : MonoBehaviour
    {
        [SerializeField] private List<RoutePoint> points;
        
        public RoutePoint RootPoint { get; private set; }
        public RoutePoint DestinationPoint { get; private set; }

        public event EventHandler<bool> RootPointCameraVisibleChanged;

        public void Initialize()
        {
            if (points.Count == 0)
                throw new NullReferenceException("Route haven't any point");

            for (int i = 0; i < points.Count - 1; i++)
            {
                points[i].NextPoint = points[i + 1];
                
                if (i == 0) continue;
                
                points[i].PreviousPoint = points[i - 1];
            }

            RootPoint = points.First(point => point.IsRoot);
            DestinationPoint = points.First(point => point.IsLast);

            RootPoint.VisibleChanged += OnRootPointCameraVisibleChanged;
        }

        private void OnRootPointCameraVisibleChanged(object sender, bool isVisible)
        {
            if (sender as RoutePoint == RootPoint) 
                RootPointCameraVisibleChanged?.Invoke(this, isVisible);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;
            if (RootPoint == null) return;
            
            Gizmos.color = Color.white;
            DrawPointLines();
        }
        
        private void DrawPointLines()
        {
            RoutePoint currentPoint = RootPoint;

            while (!currentPoint.IsLast)
            {
                Gizmos.DrawLine(currentPoint.transform.position, currentPoint.NextPoint.transform.position);
                currentPoint = currentPoint.NextPoint;
            }
        }
#endif
    }
}