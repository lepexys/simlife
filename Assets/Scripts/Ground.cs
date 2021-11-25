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
    public int laynum,minuslays = 10,total_y = 10,total_x = 20;
    int cur_x,cur_y;

    public void Build_level(Game game, int num)
    {
        laynum = num;
        float units = game.units;
        Vector3 curpos = game.transform.position;
        cur_x = (int)(curpos.x - (curpos.x / units - Mathf.Round(curpos.x / units))*units);
        cur_y = (int)(curpos.y - (curpos.y / units - Mathf.Round(curpos.y / units))*units);
        Lays = new List<List<Lay>>();
        for (int i = 0; i < total_x; i++)
        {
            Lays.Add(new List<Lay>());
            for (int j = 0; j < total_y; j++)
            {
                Lays[i].Add(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                Lays[i][j].Construct(game
                    ,laynum
                    , cur_x + game.pxwidth * units * (i - minuslays)
                    , cur_y - units * j);
            }
        }
    }
    public void Load_new(Game game)
    {
        float units = game.units;
        Vector3 curpos = game.transform.position;
        curpos.x = (int)(curpos.x - (curpos.x / units - Mathf.Round(curpos.x / units)) * units);
        curpos.y = (int)(curpos.y - (curpos.y / units - Mathf.Round(curpos.y / units)) * units);
        if (curpos.x>cur_x)
        {
            int steps = (int)Mathf.Round((curpos.x - cur_x) / units);
            for (int i = 0; i < steps; i++)
                Lays.RemoveAt(i);
            int offset = Lays.Count;
            for (int i = offset;i< total_x; i++)
            {
                Lays.Add(new List<Lay>());
                for(int j = 0;j<total_y;j++)
                {
                    Lays[i].Add(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                    Lays[i][j].Construct(game
                        ,laynum
                        , cur_x + game.pxwidth * units * (i-minuslays)
                        , cur_y - units * j);
                }
            }
        }
        else
        {
            int steps = (int)Mathf.Round((cur_x-curpos.x) / units);
            int offset = Lays.Count-steps;
            for (int i = 0; i < steps; i++)
                Lays.RemoveAt(offset+i);
            for (int i = 0; i < steps; i++)
            {
                Lays.Insert(i,new List<Lay>());
                for (int j = 0; j < total_y; j++)
                {
                    Lays[i].Add(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                    Lays[i][j].Construct(game
                        ,laynum
                        , cur_x + game.pxwidth * units * (i - minuslays)
                        , cur_y - units * j);
                }
            }
        }
        cur_x = (int)curpos.x;
        cur_y = (int)curpos.y;
    }
}
