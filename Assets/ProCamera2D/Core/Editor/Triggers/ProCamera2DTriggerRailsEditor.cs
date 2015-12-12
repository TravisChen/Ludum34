using UnityEditor;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    [CustomEditor(typeof(ProCamera2DTriggerRails))]
    [CanEditMultipleObjects]
    public class ProCamera2DTriggerRailsEditor : Editor
    {
        void OnEnable()
        {
            var proCamera2DTriggerRails = (ProCamera2DTriggerRails)target;

            if(proCamera2DTriggerRails.ProCamera2D == null && Camera.main != null)
                proCamera2DTriggerRails.ProCamera2D = Camera.main.GetComponent<ProCamera2D>();

            if(proCamera2DTriggerRails.ProCamera2D != null && proCamera2DTriggerRails.ProCamera2DRails == null)
                proCamera2DTriggerRails.ProCamera2DRails = proCamera2DTriggerRails.ProCamera2D.GetComponentInChildren<ProCamera2DRails>();
        }
    }
}