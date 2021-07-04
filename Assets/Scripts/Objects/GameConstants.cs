using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/gameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public float groundSurface = -1.0f;

    public int breakBrickNumDebris = 5;

    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    public float spawnMinX = -20.0f;
    public float spawnMaxX = -10.0f;

    public float enemyMaxOffset = 5.0f;
    public float enemyPatrolTime = 2.0f;

    public Dictionary<string, Vector2> levelStartPoint = new Dictionary<string, Vector2>{
        ["MarioScene2"] = new Vector2(-18.0f, 5.5f)
    };
}
