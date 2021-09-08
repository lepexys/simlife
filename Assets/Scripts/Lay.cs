using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Lay : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    private Spline spline;
    public void Construct(float x,float y, int width, int heigh, float unit)
    {
        spriteShapeController = GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;
        spline.Clear();
        for (int i = 0;i<width+1;i++)
        {
            spline.InsertPointAt(i, new Vector3(x+i*unit, y));
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }
        spline.InsertPointAt(width+1, new Vector3(x + width * unit, y - heigh * unit));
        spline.SetTangentMode(width + 1, ShapeTangentMode.Continuous);
        spline.SetLeftTangent(width + 1, new Vector3(1, 0, 0));
        spline.SetRightTangent(width + 1, new Vector3(-1, 0, 0));
        spline.InsertPointAt(width+2, new Vector3(x, y - heigh * unit));
        spline.SetTangentMode(width + 2, ShapeTangentMode.Continuous);
        spline.SetLeftTangent(width + 2, new Vector3(1, 0, 0));
        spline.SetRightTangent(width + 2, new Vector3(-1, 0, 0));
        spriteShapeController.BakeCollider();
    }
    private void Update()
    {
        int pointCount = spline.GetPointCount();
        for (var i = 0; i < pointCount; i++)
        {
            Vector3 currentPointPos = spline.GetPosition(i);
        }
    }
}
