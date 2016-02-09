using UnityEngine;
using System.Collections;

/// <summary>
/// @author Marshall Mason
/// @description storage class for character data
/// </summary>
public enum CharacterSpecial
{
    NONE,
    SPECIAL_ONE,
    SPECIAL_TWO
}
public class ScriptCharacterData
{
    public CharacterSpecial special = CharacterSpecial.NONE;
    public float cartCapacityFactor = 0f;
    public float armLength = 1.5f;
    public float handHitBoxSize = 1f;
    public float reachSpeed = 1f;
    public float trackingSpeed = 1f;
}
