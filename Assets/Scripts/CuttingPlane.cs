using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPlane : MonoBehaviour
{
    public EzySlice.Plane _cuttingPlane;
    // Start is called before the first frame update
    void Awake()
    {
        _cuttingPlane = new EzySlice.Plane();
    }

    public void OnDrawGizmos()
    {
        //EzySlice.Plane cuttingPlane = new EzySlice.Plane();

        // the plane will be set to the same coordinates as the object that this
        // script is attached to
        // NOTE -> Debug Gizmo drawing only works if we pass the transform
        _cuttingPlane.Compute(transform);

        // draw gizmos for the plane
        // NOTE -> Debug Gizmo drawing is ONLY available in editor mode. Do NOT try
        // to run this in the final build or you'll get crashes (most likey)
        _cuttingPlane.OnDebugDraw();
    }
}
