using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    private Vector2 pos;
    private bool isFree;

    /*CONSTRUCTOR*/
    public Position(Vector2 p)
    {
        pos = p;
        isFree = true;
    }

    /*GETTERS AND SETTERS*/
    public bool getIsFree()
    {
        return isFree;
    }

    public void setIsFree(bool f)
    {
        isFree = f;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public void setPos(Vector2 p)
    {
        pos = p;
    }
}
