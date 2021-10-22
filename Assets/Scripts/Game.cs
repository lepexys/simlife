using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Game : MonoBehaviour
{
    private List<Ground> ground = new List<Ground>();
    public Ground fabG;
    public Man fabM;
    int cur_ground = 0, min_ground = -6, max_ground = 6;
    float dist_between_layers = 1.5f;
    void Start()
    {
        fabM = Instantiate(this.fabM, this.transform.position, this.transform.rotation);
        for (int i = min_ground; i<= max_ground; i++)
            ground.Add(Instantiate(this.fabG, this.transform.position+new Vector3(0,0, dist_between_layers * i), this.transform.rotation));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (cur_ground < max_ground)
            {
                fabM.Move(dist_between_layers);
                cur_ground++;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (cur_ground > min_ground)
            {
                fabM.Move(-dist_between_layers);
                cur_ground--;
            }
        }
    }
}
