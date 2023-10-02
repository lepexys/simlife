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

    private int width = 1024;
    private int height = 1024;
    int root = 0;
    private float sizeScale = 100.0f;

    private float offsetX = 0.0f;
    private float offsetY = 0.0f;
    public Texture2D GeneratePerlinNoize()
    {
        textureSourse.Resize(width, height);
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                textureSourse.SetPixel(x, y, color);
            }
        textureSourse.Apply();
        return textureSourse;
    }
    //Calculates color on the height map
    public Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * sizeScale + offsetX;
        float yCoord = (float)y / height * sizeScale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
    public void Set_root(int r)
    {
        root = r;
        offsetX = getRand(root);
        offsetY = getRand((int)offsetX);
    }
    public int getRand(int X)
    {
        return (1283 + X * 106) % 6075;
    }
}

public class Game : MonoBehaviour
{
    enum GameState
    {
        NotStarted = 0,
        Started = 1
    }
    public float units = 0.5f;
    private List<Ground> ground = new List<Ground>();
    Vector3 offset = new Vector3(0f, 1f, -15f);
    public Camera cam;
    public Ground fabG;
    public Man fabM,
        man;
    public int cur_ground = 0 
        , min_ground = -4
        , max_ground = 4;
    public float dbl = 1.5f //distance between layers
        , layer_depth = 0.5f;
    int root;
    public float layer_height_scale = 2.0f
        , player_offset = 2.0f;
    public Vector2 curPos;
    GameState gameState = GameState.NotStarted;
    void Start()
    {
        root = (int)Random.Range(0.0f, 1000000.0f);
        PerlinNoizeGenerator.Instance.Set_root(root);
        man = Instantiate(this.fabM, new Vector3(0, getHeight(0, cur_ground) + player_offset, cur_ground), this.transform.rotation);
        for (int i = min_ground; i <= max_ground; i++)
        {
            ground.Add(Instantiate(this.fabG, this.transform.position + new Vector3(0, 0, dbl * i), this.transform.rotation));
            ground[i - min_ground].Initialize(this,i);
        }
    }
    public float getHeight(int x, int y)
    {
        return PerlinNoizeGenerator.Instance.CalculateColor(x, y).r * layer_height_scale;
    }
    private void CameraFollow()
    {
        cam.transform.position = man.torso.transform.position + offset;
    }
    void Update()
    {
        if (gameState == GameState.NotStarted)
        {
            if(man.torso != null)
            {
                CameraFollow();
                curPos = man.torso.transform.position;
                gameState = GameState.Started;
            }
        }
        else if (gameState == GameState.Started)
        {
            CameraFollow();
            if (Mathf.Abs(man.torso.transform.position.x - curPos.x) > units)
            {
                curPos.x = man.torso.transform.position.x;
                for (int i = min_ground; i <= max_ground; i++)
                {
                    ground[i - min_ground].Load_new(this, true);
                }
            }
            if (Mathf.Abs(man.torso.transform.position.y - curPos.y) > units)
            {
                curPos.y = man.torso.transform.position.y;
                for (int i = min_ground; i <= max_ground; i++)
                {
                    ground[i - min_ground].Load_new(this, false);
                }
            }
            //key actions
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                man.state = State.Walk;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                man.state = State.Jump;
            }
            //key down actions
            if (Input.GetKeyDown(KeyCode.Space))
            {
                man.state = State.Jump;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (cur_ground < max_ground)
                {
                    man.Move(dbl);
                    cur_ground++;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (cur_ground > min_ground)
                {
                    man.Move(-dbl);
                    cur_ground--;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!man.is_left)
                    man.state = State.Turn;
                else
                    man.state = State.Walk;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (man.is_left)
                    man.state = State.Turn;
                else
                    man.state = State.Walk;
            }
            //key up actions
            if (Input.GetKeyUp(KeyCode.Space))
            {
                man.state = State.Idle;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                man.walk_cicle = 0.0f;
                man.leg_high_l.targetRotation = 0.0f;
                man.leg_high_r.targetRotation = 0.0f;
                man.leg_low_r.targetRotation = 0.0f;
                man.leg_low_l.targetRotation = 0.0f;
                if (man.state == State.Walk)
                    man.state = State.Idle;
            }
        }
    }
}
