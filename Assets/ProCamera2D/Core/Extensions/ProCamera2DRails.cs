using UnityEngine;
using System.Collections.Generic;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public enum FollowMode
    {
        BothAxis,
        HorizontalAxis,
        VerticalAxis
    }

    public class ProCamera2DRails : BasePC2D
    {
        [HideInInspector]
        public List<Vector3> RailNodes = new List<Vector3>();

        public FollowMode FollowMode;

        public List<CameraTarget> CameraTargets = new List<CameraTarget>();
        Dictionary<CameraTarget, Transform> CameraTargetsOnRails = new Dictionary<CameraTarget, Transform>();

        KDTree _kdTree;
        
        List<CameraTarget> _tempCameraTargets = new List<CameraTarget>();

        override protected void Start()
        {
            base.Start();

            _kdTree = KDTree.MakeFromPoints(RailNodes.ToArray());

            for (int i = 0; i < CameraTargets.Count; i++)
            {
                var railTransform = new GameObject(CameraTargets[i].TargetTransform.name + "_OnRails").transform;
                CameraTargetsOnRails.Add(
                    CameraTargets[i],
                    railTransform
                    );

                var cameraTarget = ProCamera2D.AddCameraTarget(railTransform);
                cameraTarget.TargetOffset = CameraTargets[i].TargetOffset;
            }

            if (CameraTargets.Count == 0)
            {
                Debug.LogWarning("No targets defined!");
                enabled = false;
            }
        }

        void Update()
        {
            var pos = Vector3.zero;
            for (int i = 0; i < CameraTargets.Count; i++)
            {
                switch (FollowMode)
                {
                    case FollowMode.HorizontalAxis:
                        pos = VectorHVD(
                            Vector3H(CameraTargets[i].TargetPosition) * CameraTargets[i].TargetInfluenceH, 
                            Vector3V(ProCamera2D.CameraPosition), 
                            0);
                        break;

                    case FollowMode.VerticalAxis:
                        pos = VectorHVD(
                            Vector3H(ProCamera2D.CameraPosition), 
                            Vector3V(CameraTargets[i].TargetPosition) * CameraTargets[i].TargetInfluenceV, 
                            0);
                        break;

                    case FollowMode.BothAxis:
                        pos = VectorHVD(
                            Vector3H(CameraTargets[i].TargetPosition) * CameraTargets[i].TargetInfluenceH, 
                            Vector3V(CameraTargets[i].TargetPosition) * CameraTargets[i].TargetInfluenceV,
                            0);
                        break;
                }
                CameraTargetsOnRails[CameraTargets[i]].position = GetPositionOnRail(pos);
            }
        }
        
        public void DisableTargets(float transitionDuration = 0f)
        {
            if(_tempCameraTargets.Count != 0)
                return;
                
            for (int i = 0; i < CameraTargetsOnRails.Count; i++)
            {
                ProCamera2D.RemoveCameraTarget(CameraTargetsOnRails[CameraTargets[i]], transitionDuration);
                _tempCameraTargets.Add(ProCamera2D.AddCameraTarget(CameraTargets[i].TargetTransform, CameraTargets[i].TargetInfluenceH, CameraTargets[i].TargetInfluenceV, transitionDuration));
            }
        }
        
        public void EnableTargets(float transitionDuration = 0f)
        {
            for (int i = 0; i < _tempCameraTargets.Count; i++)
            {
                ProCamera2D.RemoveCameraTarget(_tempCameraTargets[i].TargetTransform, transitionDuration);
                ProCamera2D.AddCameraTarget(CameraTargetsOnRails[CameraTargets[i]], 1, 1, transitionDuration);
            }
            
            _tempCameraTargets.Clear();
        }

        Vector3 GetPositionOnRail(Vector3 pos)
        {
            var closestNode = _kdTree.FindNearest(pos);

            if (closestNode == 0)
            {
                return GetPositionOnRailSegment(RailNodes[0], RailNodes[1], pos);
            }
            else if (closestNode == RailNodes.Count - 1)
            {
                return GetPositionOnRailSegment(RailNodes[RailNodes.Count - 1], RailNodes[RailNodes.Count - 2], pos);
            }
            else
            {
                var leftSeg = GetPositionOnRailSegment(RailNodes[closestNode - 1], RailNodes[closestNode], pos);
                var rightSeg = GetPositionOnRailSegment(RailNodes[closestNode + 1], RailNodes[closestNode], pos);

                if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
                    return leftSeg;
                else
                    return rightSeg;
            }
        }

        Vector3 GetPositionOnRailSegment(Vector3 node1, Vector3 node2, Vector3 pos)
        {
            var node1ToPos = pos - node1;
            var segmentDirection = (node2 - node1).normalized;

            float node1Dot = Vector3.Dot(segmentDirection, node1ToPos);

            if (node1Dot < 0f)
            {
                return node1;
            }
            else if (node1Dot * node1Dot > (node2 - node1).sqrMagnitude)
            {
                return node2;
            }
            else
            {
                var fromNode1 = segmentDirection * node1Dot;
                return node1 + fromNode1;
            }
        }

#if UNITY_EDITOR
        int _drawGizmosCounter;

        override protected void OnDrawGizmos()
        {
            // HACK to prevent Unity bug on startup: http://forum.unity3d.com/threads/screen-position-out-of-view-frustum.9918/
            _drawGizmosCounter++;
            if (_drawGizmosCounter < 5 && UnityEditor.EditorApplication.timeSinceStartup < 60f)
                return;

            base.OnDrawGizmos();

            if (_gizmosDrawingFailed)
                return;

            var cameraDimensions = Utils.GetScreenSizeInWorldCoords(ProCamera2D.GameCamera, Mathf.Abs(Vector3D(transform.localPosition)));

            Gizmos.color = EditorPrefsX.GetColor(PrefsData.RailsColorKey, PrefsData.RailsColorValue);
            
            if(_tempCameraTargets.Count > 0)
                Gizmos.color = Gizmos.color * .7f;

            // Rails nodes
            for (int i = 0; i < RailNodes.Count; i++)
            {
                Gizmos.DrawWireCube(RailNodes[i], cameraDimensions * .01f);
            }

            // Rails path
            if (RailNodes.Count >= 2)
            {
                for (int i = 0; i < RailNodes.Count - 1; i++)
                {
                    Gizmos.DrawLine(RailNodes[i], RailNodes[i + 1]);
                }
            }
        }
#endif
    }
}