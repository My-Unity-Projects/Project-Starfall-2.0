using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn
{
    private Vector2 spawnPoint;
    private Position[] positions = new Position[3];

    /*CONSTRUCTOR*/
    public EnemySpawn(Vector2 sp, Position p1, Position p2, Position p3)
    {
        spawnPoint = sp;

        positions[0] = p1;
        positions[1] = p2;
        positions[2] = p3;
    }

    /*GETTERS AND SETTERS*/
    public Vector2 getSpawnPoint()
    {
        return spawnPoint;
    }

    public void setSpawnPoint(Vector2 sp)
    {
        spawnPoint = sp;
    }

    public Position[] getPositions()
    {
        return positions;
    }

    public void setPositions(Position p1, Position p2, Position p3)
    {
        positions[0] = p1;
        positions[1] = p2;
        positions[2] = p3;
    }
}
