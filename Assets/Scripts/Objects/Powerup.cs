using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "ScriptableObjects/Powerup", order = 2)]
public class Powerup : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public PowerupIndex index;
    public Texture powerupTexture;

    public int absoluteSpeedBooster;
    public int absoluteJumpBooster;

    public int duration;
}