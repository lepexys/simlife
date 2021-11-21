using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class PerlinNoizeGenerator
{
    private static PerlinNoizeGenerator s_Instance = null;
    public static PerlinNoizeGenerator Instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = new PerlinNoizeGenerator();
            }
            return s_Instance;
        }
    }
    private Texture2D textureSourse = null;

    private int width = 256;
    private int height = 32;

    private float sizeScale = 8.0f;

    private float offsetX = 0.0f;
    private float offsetY = 0.0f;
    public static Texture2D GeneratePerlinNoize(Texture2D textureSourse
        , int width
        , int height
        , float scale
        , float offsetX
        , float offsetY)
    {
        textureSourse.Resize(width, height);
        for(int x = 0;x<width;x++)
            for(int y = 0;y<height;y++)
            {
                Color color = CalculateColor(width, height, x, y, scale, offsetX, offsetY);
                textureSourse.SetPixel(x, y, color);
            }
        textureSourse.Apply();
        return textureSourse;
    }
    public Texture2D GeneratePerlinNoize()
    {
        textureSourse.Resize(width, height);
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(width, height, x, y, sizeScale, offsetX, offsetY);
                textureSourse.SetPixel(x, y, color);
            }
        textureSourse.Apply();
        return textureSourse;
    }
    public static Color CalculateColor (int x
        , int y
        , int width
        , int height
        ,float scale
        , float offsetX
        , float offsetY)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
    public Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * sizeScale + offsetX;
        float yCoord = (float)y / height * sizeScale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}

public class Game : MonoBehaviour
{
    private float units = 0.5f;
    private List<Ground> ground = new List<Ground>();
    Vector3 offset = new Vector3(0f, 2f, -1f);
    public Camera cam;
    [Range(0f, 1f)]public float interpolation;
    public Ground fabG;
    public Man fabM;
    int cur_ground = 0, 
        min_ground = -6, 
        max_ground = 6;
    float dbl = 1.5f; //distance between layers
    int root;
    void Start()
    {
        root = (int)Random.Range(0.0f, 1000000.0f);
        fabM = Instantiate(this.fabM, this.transform.position, this.transform.rotation);
        for (int i = min_ground; i <= max_ground; i++)
        {
            ground.Add(Instantiate(this.fabG, this.transform.position + new Vector3(0, 0, dbl * i), this.transform.rotation));
            ground[i - min_ground].Set_root(i,root);
            ground[i - min_ground].Build_level(this.transform.position, units);
        }
    }
    private void CameraFollow()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, fabM.torso.transform.position + offset,interpolation);
    }
    void fixedUpdate()
    {
        CameraFollow();
    }
    void Update()
    {
        if (Mathf.Abs(cam.transform.position.x - this.transform.position.x)>units*2)
        {
            this.transform.position = cam.transform.position;
            ground[cur_ground - min_ground].Build_level(this.transform.position, units);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (cur_ground < max_ground)
            {
                fabM.Move(dbl);
                offset.z += dbl;
                cur_ground++;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (cur_ground > min_ground)
            {
                fabM.Move(-dbl);
                offset.z -= dbl;
                cur_ground--;
            }
        }
    }
}
