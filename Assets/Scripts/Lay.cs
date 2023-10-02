using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Lay : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private BoxCollider bCollider;
    private void OnDestroy()
    {
        Destroy(gameObject);
        Destroy(bCollider);
    }
    public void Construct(Game game, float x, float y, float z, bool is_grass)
    {
        float offset = game.units;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(is_grass ? "Sprites/Grass" : "Sprites/Dirt");
        spriteRenderer.transform.position = new Vector3(x, y, z);
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        bCollider = GetComponent<BoxCollider>();
        Bounds bounds = bCollider.bounds;
        bounds.SetMinMax(new Vector3(x, y, z - game.layer_depth), new Vector3(x + offset, y - offset, z + game.layer_depth));
    }
    //public void Construct(Game game, int laynum, float x, float y) 
    //{
    //    float unit = game.units,
    //        y_last = 0; //last y
    //    int curve_width = game.block_width;
    //    spriteShapeController = GetComponent<SpriteShapeController>();
    //    spline = spriteShapeController.spline;
    //    spline.Clear();
    //    int point_index = 0;
    //    int first_nonzero_column = -1
    //        ,last_nonzero_column = 0;
    //    float epsilon = 0.1f;
    //    for (int i = 0; i < curve_width * 2; i+=2)
    //    {
    //        float y_map = PerlinNoizeGenerator.Instance.CalculateColor((int)(x + (i + 1) * unit), laynum).r * game.layer_height_scale - game.layer_offset;
    //        if (y < y_map)
    //        {
    //            if (i == 0)
    //            {
    //                spline.InsertPointAt(point_index, new Vector3(x + i * unit, y));
    //                spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //                spline.SetLeftTangent(point_index, new Vector3(-1, 0, 0));
    //                spline.SetRightTangent(point_index, new Vector3(1, 0, 0));
    //                spline.InsertPointAt(point_index + 1, new Vector3(x + (i + 2) * unit, y));
    //                spline.SetTangentMode(point_index + 1, ShapeTangentMode.Continuous);
    //                spline.SetLeftTangent(point_index + 1, new Vector3(-1, 0, 0));
    //                spline.SetRightTangent(point_index + 1, new Vector3(1, 0, 0));
    //                point_index += 2;
    //            }
    //            else
    //            {
    //                spline.InsertPointAt(point_index, new Vector3(x + (i + 2) * unit, y));
    //                spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //                spline.SetLeftTangent(point_index, new Vector3(-1, 0, 0));
    //                spline.SetRightTangent(point_index, new Vector3(1, 0, 0));
    //                point_index++;
    //            }
    //            y_last = y;
    //            if (first_nonzero_column == -1)
    //            {
    //                first_nonzero_column = i;
    //            }
    //            last_nonzero_column = i;
    //        }
    //        else
    //        {
    //            if (y - unit * curve_width * 2 < y_map)
    //            {
    //                if (first_nonzero_column == 0 && last_nonzero_column == 0)
    //                {
    //                    spline.InsertPointAt(point_index, new Vector3(x + first_nonzero_column * unit, y - unit * curve_width * 2));
    //                    spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //                    spline.SetLeftTangent(point_index, new Vector3(-1, 0, 0));
    //                    spline.SetRightTangent(point_index, new Vector3(1, 0, 0));
    //                    spline.InsertPointAt(point_index + 1, new Vector3(x + i * unit, y - unit * curve_width * 2));
    //                    spline.SetTangentMode(point_index + 1, ShapeTangentMode.Continuous);
    //                    spline.SetLeftTangent(point_index + 1, new Vector3(-1, 0, 0));
    //                    spline.SetRightTangent(point_index + 1, new Vector3(1, 0, 0));
    //                    point_index += 2;
    //                }
    //                if (i == 0 || (y_last - y_map) > epsilon)
    //                {
    //                    spline.InsertPointAt(point_index, new Vector3(x + i * unit, y_map));
    //                    spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //                    spline.SetLeftTangent(point_index, new Vector3(-1, 0, 0));
    //                    spline.SetRightTangent(point_index, new Vector3(1, 0, 0));
    //                    spline.InsertPointAt(point_index + 1, new Vector3(x + (i + 2) * unit, y_map));
    //                    spline.SetTangentMode(point_index + 1, ShapeTangentMode.Continuous);
    //                    spline.SetLeftTangent(point_index + 1, new Vector3(-1, 0, 0));
    //                    spline.SetRightTangent(point_index + 1, new Vector3(1, 0, 0));
    //                    point_index += 2;
    //                }
    //                else
    //                {
    //                    if ((y_last - y_map) > epsilon)
    //                    {
    //                        spline.InsertPointAt(point_index, new Vector3(x + (i + 2) * unit, y_map));
    //                        spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //                        spline.SetLeftTangent(point_index, new Vector3(-1, 0, 0));
    //                        spline.SetRightTangent(point_index, new Vector3(1, 0, 0));
    //                        point_index++;
    //                    }
    //                }
    //                y_last = y_map;
    //                if (first_nonzero_column == -1)
    //                {
    //                    first_nonzero_column = i;
    //                }
    //                last_nonzero_column = i;
    //            }
    //            else
    //                continue;
    //        }
    //    }
    //    if (spline.GetPointCount() != 0)
    //    {
    //        if ((spline.GetPosition(0).y - (y - unit * curve_width * 2)) > epsilon)
    //        {
    //            Debug.Log(string.Format("Right point. Target: {0}\n Actual: {1}", y - unit * curve_width * 2, spline.GetPosition(0).y));
    //            spline.InsertPointAt(point_index, new Vector3(x + (last_nonzero_column + 2) * unit, y - unit * curve_width * 2));
    //            spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //            spline.SetLeftTangent(point_index, new Vector3(1, 0, 0));
    //            spline.SetRightTangent(point_index, new Vector3(-1, 0, 0));
    //            point_index++;
    //        }
    //        if ((y_last - (y - unit * curve_width * 2)) > epsilon)
    //        {
    //            Debug.Log(string.Format("Left point. Target: {0}\n Actual: {1}", y - unit * curve_width * 2, y_last));
    //            spline.InsertPointAt(point_index, new Vector3(x + first_nonzero_column * unit, y - unit * curve_width * 2));
    //            spline.SetTangentMode(point_index, ShapeTangentMode.Continuous);
    //            spline.SetLeftTangent(point_index, new Vector3(1, 0, 0));
    //            spline.SetRightTangent(point_index, new Vector3(-1, 0, 0));
    //        }
    //        spriteShapeController.BakeCollider();
    //    }
    //}
    //private void Update()
    //{
    //    int pointCount = spline.GetPointCount();
    //    for (var i = 0; i < pointCount; i++)
    //    {
    //        Vector3 currentPointPos = spline.GetPosition(i);
    //    }
    //}
}
