using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Ground : MonoBehaviour
{
    private LinkedList<LinkedList<Lay>> Lays;
    public Lay fab;
    public int layer_number = 0, //layer number (depth)
        total_y = 10, //height in Lays (TM)
        total_x = 50; //width in Lays
    int x_minuslays = 0; //offset in Lays
    float x_cur, //left-right
        y_cur; //bottom-top
    float units = .0f;
    public void Initialize(Game game, int layer_num)
    {
        x_minuslays = total_x / 2;
        layer_number = layer_num;
        units = game.units;
        Vector3 curpos = game.curPos;
        x_cur = curpos.x - (curpos.x / units - (int)(curpos.x / units)) * units;
        y_cur = curpos.y - (curpos.y / units - (int)(curpos.y / units)) * units;
        Lays = new LinkedList<LinkedList<Lay>>();
        LoadX(game, total_x, true);
    }
    public void Load_new(Game game, bool is_x) // LOAD ONLY TOP BLOCKS AND MAKE MANY LAYERS!! 
    {
        Vector3 curpos = game.curPos;
        curpos.x = curpos.x - (curpos.x / units - (int)(curpos.x / units)) * units;
        curpos.y = curpos.y - (curpos.y / units - (int)(curpos.y / units)) * units;

        if (is_x)
        {
            if (curpos.x > x_cur)
            {
                int addition = (int)((curpos.x - x_cur) / units);
                for (int i = 0; i < addition; i++)
                {
                    foreach (Lay lay in Lays.First.Value)
                    {
                        Destroy(lay);
                    }
                    Lays.RemoveFirst();
                }
                LoadX(game, addition, true);
            }
            else
            {
                int addition = (int)((x_cur - curpos.x) / units);
                int offset = Lays.Count;
                for (int i = offset - addition; i < offset; i++)
                {
                    foreach (Lay lay in Lays.Last.Value)
                    {
                        Destroy(lay);
                    }
                    Lays.RemoveLast();
                }
                LoadX(game, addition, false);
            }
        }
        else
        {
            if (curpos.y > y_cur)
            {
                foreach (LinkedList<Lay> lay in Lays)
                {
                    if(lay.Last == null)
                    {
                        LoadY(game);
                    }
                    else if(lay.Last.Value.transform.position.y < curpos.y - total_y * units)
                    {
                        Destroy(lay.Last.Value);
                        lay.RemoveLast();
                    }
                }
            }
            else
            {
                foreach (LinkedList<Lay> lay in Lays)
                {
                    if (lay.Last == null)
                    {
                        LoadY(game);
                    }
                    else if (lay.Last.Value.transform.position.y > curpos.y)
                    {
                        Destroy(lay.Last.Value);
                        lay.RemoveLast();
                    }
                }
            }
        }
        x_cur = curpos.x;
        y_cur = curpos.y;
    }
    private void LoadY(Game game)
    {
        int i = 0;
        foreach (LinkedList<Lay> lay in Lays)
        {
            for (int j = 0; j < total_y; j++)
            {
                if (lay.First == null)
                {
                    float x = x_cur + units * (i - x_minuslays)
                        , y = y_cur - units * j
                        , z = game.dbl * layer_number;
                    float y_map = game.getHeight((int)x, (int)z);
                    if (y_map >= y && y_map - units < y)
                    {
                        lay.AddFirst(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                        lay.Last.Value.Construct(game, x, y, z, true);
                    }
                }
            }
            i++;
        }
    }
    private void LoadX(Game game, int amount, bool is_add_last)
    {
        for (int i = (is_add_last ? total_x - amount : amount); (is_add_last ? (i < total_x) : (i > 0)); i += (is_add_last ? 1 : (-1)))
        {
            if (is_add_last)
            {
                Lays.AddLast(new LinkedList<Lay>());
            }
            else
            {
                Lays.AddFirst(new LinkedList<Lay>());
            }
            int cur_index = 0;
            for (int j = 0; j < total_y; j++)
            {
                float x = x_cur + units * (i - x_minuslays)
                    , y = y_cur - units * j
                    , z = game.dbl * layer_number;
                float y_map = game.getHeight((int)x, (int)z);
                if (y_map >= y && y_map - units < y)
                {
                    Lays.Last.Value.AddFirst(Instantiate(this.fab, this.transform.position, this.transform.rotation));
                    Lays.Last.Value.Last.Value.Construct(game, x, y, z, true);
                }
            }
        }
    }
}
