using UnityEngine;

/// <summary>
/// @author Marshall Mason
/// @description storage class for waypoint data
/// </summary>
public enum MoveType
{
    WAIT,
    STRAIGHT,
    BEZIER
}
public class ScriptWaypoint
{
    public MoveType moveType;
    public float moveTime;
    public Transform moveTarget;
    public Transform curvePoint;
}
