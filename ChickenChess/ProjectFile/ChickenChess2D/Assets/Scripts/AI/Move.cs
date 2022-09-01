using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public int x;
    public int y;
    public bool success;
    public int removeX;
    public int removeY;
    public bool attack;
    public double score=-9999;

    public void setxy(int x, int y, int oldx, int oldy, bool attack = false)
    {
        this.x = x;
        this.y = y;
        this.removeX = oldx;
        this.removeY = oldy;
        this.attack = attack;
    }


}
