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
    public int laynum,minuslays = 20;
    
    public void Build_level(Vector3 curpos, float units,int width, int height)
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
                    , curpos.y - height * units * j - (curpos.y / units - Mathf.Round(curpos.y / units))
                    , width
                    , height
                    , units);
            }
        }
    }
}
