using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Ground : MonoBehaviour
{
    private List<List<Lay>> Lays;
    public Lay fab;
    public int width = 1, heigh = 1, laynum, root,minuslays = 20;
    public void Set_root(int nlay, int r)
    {
        root = r;
        laynum = nlay;
    }
    public int getRand(int X)
    {
        return (1283 + X * 106) % 6075;
    }
    public void Build_level(Vector3 curpos, float units)
    {
        Lays = new List<List<Lay>>();
        for (int i = 0; i < 28; i++)
        {
            Lays.Add(new List<Lay>());
            for (int j = 0; j < 6; j++)
            {
                Lays[i].Add(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                Lays[i][j].Construct(laynum
                    , curpos.x - (curpos.x / units - Mathf.Round(curpos.x / units)) - width * units * minuslays + width * units * i
                    , curpos.y - heigh * units * j - (curpos.y / units - Mathf.Round(curpos.y / units))
                    , width
                    , heigh
                    , units);
            }
        }
    }
}
