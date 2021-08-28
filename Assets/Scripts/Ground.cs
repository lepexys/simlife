using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Ground : MonoBehaviour
{
    private List<Lay> Lays;
    public Lay fab;
    public Vector3 curpos;
    void Start()
    {
        float units = 0.5f;
        int width = 8;
        Lays = new List<Lay>();
        for (int i = 0;i < 5; i++)
        {
            Lays.Add(Instantiate(this.fab, this.transform.position, this.transform.rotation));
            Lays[i].Construct(curpos.x - width * units * 2.5f + width * units * i, width, units);
        }
    }
    void Update()
    {
        //Update when new chunk need to be drawn
    }
}
